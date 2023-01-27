using Microsoft.EntityFrameworkCore;
using ShopMuseoProgettoFinale.Models;
using ShopMuseoProgettoFinale.UtilClasses;

namespace ShopMuseoProgettoFinale.Database
{
    public class ApplicationDbContext : DbContext {
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
            // Conversione per una data da DateTime a DateOnly, perché il database funziona soltanto con DateTime
            // quindi bisogna dirgli come convertire questi dati nel database
            modelBuilder.Entity<Purchase>(builder => {
                 builder.Property(x => x.Date)
                        .HasConversion<DateOnlyConverter, DateOnlyComparer>();
            });

            // Stessa cosa qui sotto, però per i rifornimenti
            modelBuilder.Entity<Resupply>(builder => {
                 builder.Property(x => x.Date)
                        .HasConversion<DateOnlyConverter, DateOnlyComparer>();
            });

            // Qui chiamiamo il metodo di base, perché quando viene fatto in override
            // non viene eseguito automaticamente e quindi le migrations per il login falliscono
            base.OnModelCreating(modelBuilder);
        }
    }
}
