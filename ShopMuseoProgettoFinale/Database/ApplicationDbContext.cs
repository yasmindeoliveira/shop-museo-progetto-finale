using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopMuseoProgettoFinale.Models;
using ShopMuseoProgettoFinale.UtilClasses;

namespace ShopMuseoProgettoFinale.Database {
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> {
        private const string ConnectionString = "Data Source=localhost;"
                                        + "Database=MuseumShop;"
                                        + "Integrated Security=True;"
                                        + "TrustServerCertificate=True";

        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Resupply> Resupplies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            _ = optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // Conversione per una data da DateTime a DateOnly, perché il database funziona soltanto con DateTime
            // quindi bisogna dirgli come convertire questi dati nel database
            _ = modelBuilder.Entity<Purchase>(builder => {
                _ = builder.Property(x => x.Date)
                       .HasConversion<DateOnlyConverter, DateOnlyComparer>();
            });

            // Stessa cosa qui sotto, però per i rifornimenti
            _ = modelBuilder.Entity<Resupply>(builder => {
                _ = builder.Property(x => x.Date)
                       .HasConversion<DateOnlyConverter, DateOnlyComparer>();
            });

            #region Relazioni per i likes
            _ = modelBuilder.Entity<Like>()
                        .HasKey(like => new { like.UserId, like.ProductId });

            _ = modelBuilder.Entity<Like>()
                        .HasOne(like => like.ApplicationUser)
                        .WithMany(user => user.Likes)
                        .HasForeignKey(like => like.UserId);

            _ = modelBuilder.Entity<Like>()
                        .HasOne(like => like.Product)
                        .WithMany(product => product.Likes)
                        .HasForeignKey(like => like.ProductId);
            #endregion

            // Qui chiamiamo il metodo di base, perché quando viene fatto in override
            // non viene eseguito automaticamente e quindi le migrations per il login falliscono
            base.OnModelCreating(modelBuilder);
        }

        // Aggiungi un prodotto senza pensare a dover salvare
        public int AddProduct(Product product) {
            _ = Products.Add(product);
            return SaveChanges();
        }

        // Rimuovi un prodotto senza pensare a dover salvare
        public int RemoveProduct(Product product) {
            _ = Products.Remove(product);
            return SaveChanges();
        }
    }
}
