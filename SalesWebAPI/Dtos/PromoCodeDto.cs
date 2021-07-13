using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebAPI.Dtos
{
    public class PromoCodeDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
    }
}
