using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMuseoProgettoFinale.Database;
using ShopMuseoProgettoFinale.Models;

namespace ShopMuseoProgettoFinale.Controllers {
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller {
        public IActionResult Index() {
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                List<Product> productList = db.Products.ToList();
                productList = productList.OrderBy(p => p.Quantity).ToList();
                return View("Index", productList);
            }
        }

        #region Create Product
        //---------------------------------------------------------------------
        [HttpGet]
        public IActionResult CreateProduct() {
            // Qui va aggiunto "Create" perché altrimenti viene ritornata una view chiamata CreateProduct,
            // diversa dalla view ritornata dal metodo subito sotto
            return View();
        }
        //---------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProduct(Product formData) {
            // TODO: Aggiungere un ViewModel per permettere all'admin di aggiungere una quantità iniziale
            if (!ModelState.IsValid) {

                return View(formData);
            }

            using (ApplicationDbContext db = new ApplicationDbContext()) {
                db.AddProduct(formData);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Update Product
        [HttpGet]
        public IActionResult UpdateProduct(int id) {
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                Product productFound = db.Products.Find(id);

                // Meglio "is not null" invece di != perché è più preciso e leggibil
                if (productFound is not null) {
                    return View(productFound);
                } else {
                    return NotFound($"Non è stato trovato nessun prodotto con {id}");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProduct(int id, Product formData) {
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                if (!ModelState.IsValid) {
                    return View(formData);
                } else {
                    Product productFound = db.Products.Find(id);
                    // If check per controllare che l'id sia sempre valido, altrimenti il programma crasherebbe
                    if (productFound is null) {
                        return NotFound($"Non è stato trovato nessun prodotto con {id}");
                    }

                    productFound.Name = formData.Name;
                    productFound.Price = formData.Price;
                    productFound.Description = formData.Description;
                    productFound.PictureUrl = formData.PictureUrl;
                    productFound.Quantity = formData.Quantity;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
        }
        #endregion

        #region Delete Product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int id) {

            using (ApplicationDbContext db = new ApplicationDbContext()) {
                Product productFound = db.Products.Find(id);
                if (productFound != null) {
                    db.RemoveProduct(productFound);
                    return RedirectToAction("Index");
                } else {
                    return NotFound("Il prodotto da cancellare non è stato trovato");
                }
            }
        }
        #endregion

        #region Resupplies
        [HttpGet]
        public IActionResult ViewResupplies() {
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                List<Resupply> resupplyLista = db.Resupplies.ToList();

                return View(resupplyLista);
            }

        }

        [HttpGet]
        public IActionResult ResupplyCreate(int? id) {
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                List<Product> listaProdotti = db.Products.ToList();
                ProductResupplyView newModelView = new ProductResupplyView();
                newModelView.ProductList = listaProdotti;
                newModelView.Resupply = new Resupply();

                if (id is not null) {
                    var productFound = db.Products.Find(id);
                    if (productFound is not null) {
                        newModelView.Resupply.ProductId = (int) id;
                    }
                }

                return View(newModelView);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResupplyCreate(ProductResupplyView formData) {
            formData.Resupply.Date = DateOnly.FromDateTime(DateTime.Now);

            using (ApplicationDbContext db = new ApplicationDbContext()) {
                if (!ModelState.IsValid) {

                    //per visualizzare le liste di prodotti nel momento in cui si crea domanda per Resupply
                    List<Product> listaProdotti = db.Products.ToList();
                    formData.ProductList = listaProdotti;
                    return View(formData);

                } else {

                    // Trova il prodotto
                    var foundProduct = db.Products.Find(formData.Resupply.ProductId);

                    // Aggiornane la quantità
                    foundProduct.Quantity = foundProduct.Quantity + formData.Resupply.Quantity;

                    // Salva tutte le modifiche
                    db.Resupplies.Add(formData.Resupply);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
        }
        #endregion
    }
}
