using Microsoft.EntityFrameworkCore;
using SalesWebAPI.Models;

namespace SalesWebAPI
{
    public class PostgresContext : DbContext
    {
        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<PromoCodeStatus> PromoCodeStatuses { get; set; }
        public DbSet<PromoCodeBook> PromoCodeBooks { get; set; }
    }
}
