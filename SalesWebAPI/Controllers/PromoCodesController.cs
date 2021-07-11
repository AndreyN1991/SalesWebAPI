using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebAPI;
using SalesWebAPI.Models;

namespace SalesWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromoCodesController : Controller
    {
        private readonly PostgresContext _context;

        public PromoCodesController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PromoCode>> GetPromoCode()
        {
            var promoCode = new PromoCode()
            {
                Code = GeneratePromoCode(),
                Status = _context.PromoCodeStatuses.Find(1)
            };

            _context.PromoCodes.Add(promoCode);
            await _context.SaveChangesAsync();

            return promoCode;
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<PromoCode>> GetPromoCode(string code)
        {
            var promoCode = await _context.PromoCodes.Where(x => x.Code == code).FirstOrDefaultAsync();

            if (promoCode == null)
                return NotFound();

            return promoCode;
        }

        private string GeneratePromoCode()
        {
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = string.Empty;
            var random = new Random();

            for (int i = 0; i < 6; i++)
                result += alphabet.Substring(random.Next(alphabet.Length), 1);

            if (_context.PromoCodes.Where(x => x.Code == result).Count() > 0)
                result = GeneratePromoCode();

            return result;
        }
    }
}
