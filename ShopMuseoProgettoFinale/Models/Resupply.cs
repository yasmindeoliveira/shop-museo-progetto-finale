namespace ShopMuseoProgettoFinale.Models {
    public class Resupply {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public Product PurchasedProduct { get; set; }
        public int Quantity { get; set; }
        public string SupplierName { get; set; }
        public double Price { get; set; }
    }
}
