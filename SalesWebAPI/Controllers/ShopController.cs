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
    public class ShopController : Controller
    {
        private readonly PostgresContext _context;

        public ShopController(PostgresContext postgresContext)
        {
            _context = postgresContext;
        }

        [HttpGet("{count}")]
        public async Task<ActionResult<IEnumerable<OrdersDto>>> GetOrders(int count)
        {
            return await _context.PromoCodes.Where(x => x.StatusId == 2).Take(count).Select(x => new OrdersDto()
            {
                PromoCode = x.Code,
                ISBN = _context.PromoCodeBooks.Where(cb => cb.PromoCodeId == x.Id).Join(_context.Books, b1 => b1.BookId, b2 => b2.Id, (b1, b2) => b2.ISBN).ToArray()
            }).ToListAsync();
        }

        [HttpPut("{code}")]
        public async Task<ActionResult> PutOrder(string code, string url)
        {
            var promoCode = await _context.PromoCodes.Where(x => x.Code == code).FirstOrDefaultAsync();

            if (promoCode == null)
                return BadRequest();

            promoCode.StatusId = 3;
            promoCode.URL = url;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{code}")]
        public async Task<ActionResult> DeleteOrder(string code)
        {
            var promoCode = await _context.PromoCodes.Where(x => x.Code == code).FirstOrDefaultAsync();

            if (promoCode == null)
                return BadRequest();

            _context.PromoCodes.Remove(promoCode);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
