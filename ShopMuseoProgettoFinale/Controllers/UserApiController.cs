using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMuseoProgettoFinale.Database;
using ShopMuseoProgettoFinale.Models;

namespace ShopMuseoProgettoFinale.Controllers {
    [Route("api")]
    [ApiController]
    public class UserApiController : ControllerBase {
        #region Products con ricerca
        // Crea una lista di tutti i prodotti e la manda in formato JSON 
        [HttpGet]
        [Route("products")]
        public IActionResult Products(string? search) {
            using ApplicationDbContext db = new();

            List<Product> products = db.Products.Include(p => p.Likes).ToList();
            if (search is not null && !string.IsNullOrWhiteSpace(search)) {
                products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return !products.Any() ? NotFound("Non sono stati trovati prodotti con quella stringa di ricerca.") : Ok(products);
        }
        #endregion

        #region Un product con ID
        // Trova il prodotto con quell'Id e lo manda indietro, altrimenti NotFound
        [HttpGet("{id}")]
        [Route("product/{id}")]
        public IActionResult Product(int id) {
            if (id < 1) {
                return BadRequest("L'Id non può essere minore di 1");
            }

            using ApplicationDbContext db = new();
            Product? articolo = db.Products.Find(id);

            return articolo is null ? NotFound("L'articolo non è stato trovato con questo id") : Ok(articolo);
        }
        #endregion

        #region Creazione di un acquisto nel DB
        // Riceve un JSON con le proprietà per Purchase, e crea un purchase,
        // togliendo dalla quantità di un prodotto in magazzino
        [HttpPost]
        [Route("purchase")]
        public IActionResult PurchaseCreate([FromBody] Purchase formData) {
            // I dati ricevuti saranno l'Id del prodotto, la quantità, il nome del cliente, e la data

            using ApplicationDbContext db = new();

            // Controlla che esista il prodotto nel purchase
            Product foundProduct = db.Products.Find(formData.ProductId);
            if (foundProduct is null) {
                ModelState.AddModelError("ProductId", "L'Id provveduto per il prodotto non è stato trovato.");
                return ValidationProblem(ModelState);
            }

            formData.Product = foundProduct;
            // Controlla che la quantità nel purchase non sia più di quella nel prodotto
            if (formData.Quantity > formData.Product.Quantity) {
                ModelState.AddModelError("Quantity", "La quantità in magazzino è minore della quantità dell'acquisto.");
                return ValidationProblem(ModelState);
            }

            // Non serve secondo Microsoft, perché gli API lo fanno in automatico
            //if (!ModelState.IsValid) {
            //    return BadRequest(ModelState);
            //}

            // Aggiungi il purchase al DB
            formData.Date = DateOnly.FromDateTime(DateTime.Now);
            _ = db.Purchases.Add(formData);
            formData.Product.Quantity -= formData.Quantity;
            int dbChanges = db.SaveChanges();

            return Ok(new {
                QuantityLeft = formData.Product.Quantity,
                Message = "Acquisto completato con successo!"
            });
        }
        #endregion

        #region Likes di un prodotto
        [HttpGet("likes/{id}")]
        public IActionResult Likes(int id) {
            using ApplicationDbContext db = new();

            Product? foundProduct = db.Products.Find(id);
            if (foundProduct is null) {
                return NotFound();
            }

            db.Entry(foundProduct)
              .Collection(p => p.Likes)
              .Load();

            List<Like> foundLikes = foundProduct.Likes ?? new();

            return Ok(foundLikes);
        }
        #endregion

        #region Aggiungere un like
        [HttpPut("addlike/{id}")]
        [Authorize]
        public IActionResult AddLike(int id) {
            using ApplicationDbContext db = new();

            if (db.Products.Find(id) is not Product product) {
                return NotFound();
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            db.Entry(product)
              .Collection(p => p.Likes)
              .Load();

            bool userLikedProduct = product.Likes.Any(l => l.UserId == userId);

            if (!userLikedProduct) {
                product.Likes.Add(new() {
                    UserId = userId,
                    ApplicationUser = (ApplicationUser) db.Users.Find(userId),
                    Product = product,
                    ProductId = id
                });
            }

            _ = db.SaveChanges();

            return Ok(product.Likes);
        }
        #endregion

        #region Rimuovere un like
        [HttpDelete("removelike/{id}")]
        [Authorize]
        public IActionResult RemoveLike(int id) {
            using ApplicationDbContext db = new();

            if (db.Products.Find(id) is not Product product) {
                return NotFound();
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            db.Entry(product)
              .Collection(p => p.Likes)
              .Load();

            bool userLikedProduct = product.Likes.Any(l => l.UserId == userId);

            if (userLikedProduct) {
                _ = product.Likes.RemoveAll(l => l.UserId == userId);
            }

            _ = db.SaveChanges();

            return Ok(product.Likes);
        }
        #endregion
    }
}
