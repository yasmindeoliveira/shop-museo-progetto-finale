using Microsoft.AspNetCore.Mvc;
using ShopMuseoProgettoFinale.Database;
using ShopMuseoProgettoFinale.Models;

namespace ShopMuseoProgettoFinale.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Product> productList = db.Products.ToList();
                return View("Index", productList);
            }

        }

        //---------------------------------------------------------------------
        [HttpGet]
        public IActionResult CreateProduct()
        {

            return View();
        }
        //---------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateProduct(Product formData)
        {
            if (!ModelState.IsValid)
            {

                return View("Create", formData);
            }

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Products.Add(formData);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        //---------------------------------------------------------------------
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Product productFound = db.Products.Find(id);

                if (productFound != null)
                {
                    return View("UpdateProduct", productFound);

                }
                else
                {
                    return NotFound("il prodotto non è stato trovato, non esiste");
                }
            }
        }
        //---------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProduct(int id, Product formData)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (!ModelState.IsValid)
                {

                    return View("UpdateProduct", formData);
                }
                else
                {
                    Product productFound = db.Products.Find(id);
                    productFound.Name = formData.Name;
                    productFound.Price = formData.Price;
                    productFound.Description = formData.Description;
                    productFound.PictureUrl = formData.PictureUrl;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
        }
        //---------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int id)
        {

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Product productFound = db.Products.Find(id);
                if (productFound != null)
                {
                    db.Products.Remove(productFound);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("il prodotto da cancellare non è stato trovato");
                }
            }
        }
        //---------------------------------------------------------------
        //Metodi per Purchases
        public IActionResult PurchasesView()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Purchase> purchaseList = db.Purchases.ToList();
                return View("PurchasesView", purchaseList);

            }
        }
        //---------------------------------------------------------------------
        [HttpGet]
        public IActionResult PurchaseCreate(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Product productFound = db.Products.Find(id);
                if (productFound != null)
                {
                    return NotFound("questo prodotto non puoi acquistare");
                }
                else
                {
                    Purchase newPurchase = new Purchase();
                    newPurchase.ProductId = id;
                    return View("PurchaseCreate", newPurchase);
                }
            }

        }

        [HttpPost]
        public IActionResult PurchaseCreate(Purchase formData)
        {
            //qua arriverà quanità che vuole acquistare e nome, 

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (!ModelState.IsValid)
                {
                    return View("PurchaseCreate", formData);
                }
                else
                {
                    
                    formData.Date = DateOnly.FromDateTime(DateTime.Now);
                    db.Purchases.Add(formData);
                    db.SaveChanges();

                    //ADESSO dimnuire la quantità nel magazzino del prodotto

                    int PurchasedProductId = formData.ProductId;
                    Stock aggiornaStock = db.Stocks.Where(p=>p.ProductId == PurchasedProductId).FirstOrDefault();
                    aggiornaStock.Quantity = aggiornaStock.Quantity - formData.Quantity;

                    return RedirectToAction("PurchasesView");
                }
            }

        }
        //--------------------------RESUPPLIES--------------
        [HttpGet]
        public IActionResult ViewResupplies()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Resupply> resupplyLista = db.Resupplies.ToList();

                return View(resupplyLista);
            }

        }

        [HttpGet]
        public IActionResult ResupplyCreate()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Product> listaProdotti = db.Products.ToList();
                ProductResupplyView newModelView = new ProductResupplyView();
                newModelView.ProductList = listaProdotti;

                return View(newModelView);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResupplyCreate(ProductResupplyView formData)
        {
            if (!ModelState.IsValid)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    //per visualizzare le liste di prodotti nel momento in cui si crea domanda per Resupply
                    List<Product> listaProdotti = db.Products.ToList();
                    formData.ProductList = listaProdotti;
                    return View("ResupplyCreate", formData);
                }
            }
            else
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    db.Resupplies.Add(formData.Resupply);
                    db.SaveChanges();
                    return RedirectToAction("ViewResupplies");
                }

            }
        }










    }
}
