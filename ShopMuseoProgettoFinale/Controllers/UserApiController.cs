using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMuseoProgettoFinale.Database;
using ShopMuseoProgettoFinale.Models;

namespace ShopMuseoProgettoFinale.Controllers
{

    [Route("api")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        [HttpGet]
        [Route("products")]
        public IActionResult Products()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Product> listaProdotti = db.Products.ToList<Product>();


                return Ok(listaProdotti);
            }
        }
        //----------------------------
        [Route("details")]
        public IActionResult Details(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                Product productFound = db.Products.Find(id); //ti da prodotto che ha trovato o ti da nullo. cerca se c'è un elemento con quell'id. 

                if (productFound == null)
                {
                    return NotFound("l'articolo che hai cercato non esiste");
                }
                else
                {
                    return Ok(productFound);
                }

            }
        }
        //-----------------------------------------
        [Route("Purchase")]
        [HttpGet("{id}")]
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
                    PurchaseProductView modelPurchase = new PurchaseProductView();
                    modelPurchase.Product = productFound;
                    modelPurchase.Quantity = 0;
                    return Ok(modelPurchase);
                }
            }
        }
        [Route("Purchase")]
        [HttpPost]
        public IActionResult PurchaseCreate(PurchaseProductView formData)
        {
            //qua arriverà quanità che vuole acquistare e nome, 

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (!ModelState.IsValid)
                {
                    return Ok(formData);
                }
                else
                {
                    Purchase newPurchase = new Purchase();
                    newPurchase.Date = DateOnly.FromDateTime(DateTime.Now);
                    newPurchase.Quantity = formData.Quantity;
                    newPurchase.PurchasedProduct = formData.Product;
                    db.Purchases.Add(newPurchase);

                    //ADESSO dimnuire la quantità nel magazzino del prodotto

                    int PurchasedProductId = newPurchase.PurchasedProduct.Id;
                    Stock aggiornaStock = db.Stocks.Find(PurchasedProductId);

                    aggiornaStock.Quantity = aggiornaStock.Quantity - formData.Quantity;



                    db.SaveChanges();


                    return RedirectToAction("products");
                }
            }

        }

        //-------------------------------------------------------------------



    }
}
