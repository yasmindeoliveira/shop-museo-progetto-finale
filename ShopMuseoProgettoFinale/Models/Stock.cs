using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMuseoProgettoFinale.Models {
    [Keyless]
    public class Stock {
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "La quantità di un prodotto in magazzino non può essere meno di zero.")]
        public int Quantity { get; set; }
    }
}
