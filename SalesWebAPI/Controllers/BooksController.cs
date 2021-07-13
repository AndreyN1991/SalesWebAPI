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
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(string code)
        {
            var promoCode = await _context.PromoCodes.Where(x => x.Code == code).FirstOrDefaultAsync();

            if (promoCode == null)
                return NotFound();

            return await _context.PromoCodeBooks.Where(x => x.PromoCodeId == promoCode.Id).Join(_context.Books, x => x.BookId, y => y.Id, (x, y) => y).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PromoCodeBook>> PostBook(PromoCodeBookDto promoCodeBook)
        {
            var codeBook = new PromoCodeBook()
            {
                BookId = promoCodeBook.BookId,
                PromoCodeId = promoCodeBook.PromoCodeId
            };
            _context.PromoCodeBooks.Add(codeBook);
            await _context.SaveChangesAsync();

            return codeBook;
        }

        [HttpDelete]
        public async Task<ActionResult<PromoCodeBook>> DeleteBook(PromoCodeBookDto promoCodeBook)
        {
            var codeBook = await _context.PromoCodeBooks.Where(x => x.BookId == promoCodeBook.BookId && x.PromoCodeId == promoCodeBook.PromoCodeId).FirstOrDefaultAsync();

            if (codeBook != null)
            {
                _context.PromoCodeBooks.Remove(codeBook);
                await _context.SaveChangesAsync();
            }

            return codeBook;
        }
    }
}
