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
    public class PromoCodesController : Controller
    {
        private readonly PostgresContext _context;

        public PromoCodesController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PromoCodeDto>> GetPromoCode()
        {
            var promoCode = new PromoCode()
            {
                Code = GeneratePromoCode(),
                Status = _context.PromoCodeStatuses.Find(1)
            };

            _context.PromoCodes.Add(promoCode);
            await _context.SaveChangesAsync();

            var promoCodeDto = new PromoCodeDto()
            {
                Id = promoCode.Id,
                Code = promoCode.Code,
                Status = promoCode.Status.Status
            };

            return promoCodeDto;
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<PromoCodeDto>> GetPromoCode(string code)
        {
            var promoCodeDto = await _context.PromoCodes.Join(_context.PromoCodeStatuses, x => x.Status.Id, y => y.Id, (x, y) => new PromoCodeDto() {
                Id = x.Id,
                Code = x.Code,
                Status = y.Status
            }).Where(x => x.Code == code).FirstOrDefaultAsync();

            if (promoCodeDto == null)
                return NotFound();

            return promoCodeDto;
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
