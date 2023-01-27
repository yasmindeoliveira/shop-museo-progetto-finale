using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopMuseoProgettoFinale.Models;
using ShopMuseoProgettoFinale.UtilClasses;

namespace ShopMuseoProgettoFinale.Database
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> {

        const string ConnectionString = "Data Source=localhost;"
                                        + "Database=MuseumShop;"
                                        + "Integrated Security=True;"
                                        + "TrustServerCertificate=True";

        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Resupply> Resupplies { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Purchase>(builder => {
                builder.Property(x => x.Date)
                       .HasConversion<DateOnlyConverter, DateOnlyComparer>();
            });

            modelBuilder.Entity<Resupply>(builder => {
                builder.Property(x => x.Date)
                       .HasConversion<DateOnlyConverter, DateOnlyComparer>();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
