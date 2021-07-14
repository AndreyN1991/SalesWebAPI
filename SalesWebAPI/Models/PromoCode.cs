using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesWebAPI.Models
{
    public class PromoCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public PromoCodeStatus Status { get; set; }
        public string URL { get; set; }
    }
}
