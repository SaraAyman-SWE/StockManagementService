using Microsoft.EntityFrameworkCore;
using StockExchangeService.Models.Entities;

namespace StockExchangeService.Data
{
    public class StockDataDbContext : DbContext
    {
        public StockDataDbContext(DbContextOptions<StockDataDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Order> Orders { get; set; }
    }
}
