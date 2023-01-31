using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMuseoProgettoFinale.Database;
using ShopMuseoProgettoFinale.Models;

namespace ShopMuseoProgettoFinale.Controllers {

    [Route("api")]
    [ApiController]
    public class UserApiController : ControllerBase {
        [HttpGet]
        [Route("products")]
        public IActionResult Products(string? search) {
            using ApplicationDbContext db = new ApplicationDbContext();
            
            List<Product> articoli = new List<Product>();
            if (string.IsNullOrWhiteSpace(search)) {
                articoli = db.Products.ToList();
            }
            else {
                articoli = db.Products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            return Ok(articoli);
        }


        [HttpGet("{id}")]
        [Route("product")]
        public IActionResult Product(int id) {

            if (id < 1) return BadRequest("L'Id non può essere minore di 1");

            using ApplicationDbContext db = new ApplicationDbContext();
            Product articolo = db.Products.Find(id);

            if (articolo is null) {
                return NotFound("L'articolo non è stato trovato con questo id");
            }

            return Ok(articolo);
        }

        [Route("Purchase")]
        [HttpPost]
        public IActionResult PurchaseCreate([FromBody] Purchase formData) {
            // I dati ricevuti saranno l'Id del prodotto, la quantità, il nome del cliente, e la data

            using ApplicationDbContext db = new ApplicationDbContext();

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            else {
                formData.Date = DateOnly.FromDateTime(DateTime.Now);

                if (formData.Quantity > formData.Product.Quantity) {
                    return BadRequest("La quantità in magazzino è minore della quantità dell'acquisto.");
                }
                
                db.Purchases.Add(formData);
                formData.Product.Quantity -= formData.Quantity;
                db.SaveChanges();

                return Ok();
            }
        }
    }
}
