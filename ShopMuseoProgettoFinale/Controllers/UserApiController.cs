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
        public IActionResult Products(string? search)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<Product> articoli = new List<Product>();
                if (search is null || search == "")
                {
                    articoli = db.Products.ToList<Product>();
                }
                else
                {
                    articoli = db.Products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList<Product>();
                }

                return Ok(articoli);
            }

        }


        [HttpGet("{id}")]
        [Route("products")]
        public IActionResult Products(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Product articolo = db.Products.Where(p => p.Id == id).FirstOrDefault();

                if (articolo is null)
                {
                    return NotFound("L'articolo non è stato trovato con questo id");
                }

                return Ok(articolo);
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
                    Purchase modelPurchase = new Purchase();
                    modelPurchase.ProductId = id;
                    return Ok(modelPurchase);
                }
            }
        }

        [Route("Purchase")]
        [HttpPost]
        public IActionResult PurchaseCreate([FromBody] Purchase formData)
        {
            //qua arriverà quanità che vuole acquistare e nome, 

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }
                else
                {
                   formData.Date = DateOnly.FromDateTime(DateTime.Now);
                   db.Purchases.Add(formData);

                    //ADESSO dimnuire la quantità nel magazzino del prodotto

                 
                    Stock stockDaAggiornare = db.Stocks.Find(formData.ProductId);

                    stockDaAggiornare.Quantity = stockDaAggiornare.Quantity - formData.Quantity;

                    db.SaveChanges();
                    return Ok();
                }
            }

        }

        //-------------------------------------------------------------------



    }
}
