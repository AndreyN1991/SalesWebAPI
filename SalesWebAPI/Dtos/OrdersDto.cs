using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebAPI.Dtos
{
    public class OrdersDto
    {
        public string PromoCode { get; set; }
        public string[] ISBN { get; set; }
    }
}
