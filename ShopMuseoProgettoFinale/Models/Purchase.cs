using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMuseoProgettoFinale.Models {
    public class Purchase {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Un acquisto non può avere zero o meno di zero come quantità.")]
        public int Quantity { get; set; }
    }
}
