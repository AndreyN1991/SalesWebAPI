using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesWebAPI.Models
{
    [Keyless]
    public class PromoCodeBook
    {
        [Key]
        [Column(Order = 1)]
        public PromoCode PromoCode { get; set; }
        [Key]
        [Column(Order = 2)]
        public Book Book { get; set; }
    }
}
