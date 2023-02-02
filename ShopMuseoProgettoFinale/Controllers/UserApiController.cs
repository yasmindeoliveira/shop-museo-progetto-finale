using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMuseoProgettoFinale.Database;
using ShopMuseoProgettoFinale.Models;

namespace ShopMuseoProgettoFinale.Controllers {

    [Route("api")]
    [ApiController]
    public class UserApiController : ControllerBase {

        // Crea una lista di tutti i prodotti e la manda in formato JSON 
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

            if (!articoli.Any()) {
                return NotFound("Non sono stati trovati prodotti con quella stringa di ricerca.");
            }

            return Ok(articoli);
        }

        // Trova il prodotto con quell'Id e lo manda indietro, altrimenti NotFound
        [HttpGet("{id}")]
        [Route("product/{id}")]
        public IActionResult Product(int id) {

            if (id < 1) return BadRequest("L'Id non può essere minore di 1");

            using ApplicationDbContext db = new ApplicationDbContext();
            Product articolo = db.Products.Find(id);

            if (articolo is null) {
                return NotFound("L'articolo non è stato trovato con questo id");
            }

            return Ok(articolo);
        }

        // Riceve un JSON con le proprietà per Purchase, e crea un purchase,
        // togliendo dalla quantità di un prodotto in magazzino
        [HttpPost]
        [Route("purchase")]
        public IActionResult PurchaseCreate([FromBody] Purchase formData) {
            // I dati ricevuti saranno l'Id del prodotto, la quantità, il nome del cliente, e la data

            using ApplicationDbContext db = new ApplicationDbContext();

            // Controlla che esista il prodotto nel purchase
            var foundProduct = db.Products.Find(formData.ProductId);
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
            db.Purchases.Add(formData);
            formData.Product.Quantity -= formData.Quantity;
            int dbChanges = db.SaveChanges();

            return Ok(new {
                QuantityLeft = formData.Product.Quantity,
                Message = "Acquisto completato con successo!"
            });
        }
    }
}
