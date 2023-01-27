namespace ShopMuseoProgettoFinale.Models {
    public class Resupply {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public Product PurchasedProduct { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La quantità di rifornimento non può essere negativa o zero.")]
        public int Quantity { get; set; }

        [MaxLength(64, ErrorMessage = "Il nome del supplier non può essere più di 64 caratteri.")]
        public string SupplierName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Il prezzo di un rifornimento non può essere negativo.")]
        public double Price { get; set; }
    }
}
