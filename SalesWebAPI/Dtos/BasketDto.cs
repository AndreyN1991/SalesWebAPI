using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebAPI.Dtos
{
    public class BasketDto
    {
        public int PromoCodeBookId { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Cost { get; set; }
    }
}
