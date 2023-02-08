using ShopMuseoProgettoFinale.UtilClasses;

namespace ShopMuseoProgettoFinale.Models {
    public class Product {
        [Key]
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "Il nome del prodotto non può superare una lunghezza di 100 caratteri.")]
        public string Name { get; set; }

        [MaxLength(1024, ErrorMessage = "La descrizione del prodotto non può superare una lunghezza di 1024 caratteri.")]
        public string Description { get; set; }

        [Url(ErrorMessage = "Il link all'immagine deve essere un URL formattato correttamente.")]
        [MaxLength(256, ErrorMessage = "Il link all'immagine non può essere più lungo di 256 caratteri.")]
        [EndsWith(".png", ".jpg", ".webp", ".jpeg", ErrorMessage = "Il link deve finire con l'estensione di un'immagine.")]
        public string PictureUrl { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Il prezzo non può essere negativo.")]
        public double Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La quantità del prodotto non può essere negativa.")]
        public int Quantity { get; set; }

        public List<Like>? Likes { get; set; } = new();
    }
}
