namespace ShopMuseoProgettoFinale.Models {
    public class Purchase {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public Product PurchasedProduct { get; set; }
        public int Quantity { get; set; }
    }
}
