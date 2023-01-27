using Microsoft.EntityFrameworkCore;
using ShopMuseoProgettoFinale.Models;

namespace ShopMuseoProgettoFinale.Database {
    public class ApplicationDbContext : DbContext {
        const string ConnectionString = "Data Source=localhost;"
                                        + "Database=PizzaShop;"
                                        + "Integrated Security=True;"
                                        + "TrustServerCertificate=True";

        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Resupply> Resupplies { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
