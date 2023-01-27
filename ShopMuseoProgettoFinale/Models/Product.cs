namespace ShopMuseoProgettoFinale.Models {
    public class Product {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public double Price { get; set; }
    }

    public class Purchase {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public Product PurchasedProduct { get; set; }
        public int Quantity { get; set; }
    }

    public class Resupply {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public Product PurchasedProduct { get; set; }
        public int Quantity { get; set; }
        public string SupplierName { get; set; }
        public double Price { get; set; }
    }
}
