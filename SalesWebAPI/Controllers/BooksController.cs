using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebAPI;
using SalesWebAPI.Dtos;
using SalesWebAPI.Models;

namespace SalesWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly PostgresContext _context;

        public BooksController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            return await _context.Books.Select(x => new BookDto()
            {
                Title = x.Title,
                Author = x.Author,
                Id = x.Id,
                BookCount = x.BookCount,
                Cost = x.Cost,
                ISBN = x.ISBN,
                Picture = x.Picture
            }).ToListAsync();
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<IEnumerable<BasketDto>>> GetBooks(string code)
        {
            var promoCode = await _context.PromoCodes.Where(x => x.Code == code).FirstOrDefaultAsync();

            if (promoCode == null)
                return NotFound();

            return await _context.PromoCodeBooks.Where(x => x.PromoCodeId == promoCode.Id).Join(_context.Books, x => x.BookId, y => y.Id, (x, y) => new BasketDto() {
                PromoCodeBookId = x.Id,
                BookId = y.Id,
                Author = y.Author,
                Cost = y.Cost,
                Title = y.Title
            }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PromoCodeBookDto>> PostBook(PromoCodeBookDto promoCodeBook)
        {
            var codeBook = new PromoCodeBook()
            {
                BookId = promoCodeBook.BookId,
                PromoCodeId = promoCodeBook.PromoCodeId
            };

            var book = await _context.Books.FindAsync(codeBook.BookId);
            book.BookCount--;

            _context.PromoCodeBooks.Add(codeBook);
            await _context.SaveChangesAsync();

            return new PromoCodeBookDto()
            {
                BookId = codeBook.BookId,
                PromoCodeId = codeBook.PromoCodeId
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PromoCodeBookDto>> DeleteBook(int id)
        {
            var codeBook = await _context.PromoCodeBooks.FindAsync(id);

            if (codeBook == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(codeBook.BookId);
            book.BookCount++;

            _context.PromoCodeBooks.Remove(codeBook);
            await _context.SaveChangesAsync();

            return new PromoCodeBookDto()
            {
                BookId = codeBook.BookId,
                PromoCodeId = codeBook.PromoCodeId
            };
        }
    }
}
