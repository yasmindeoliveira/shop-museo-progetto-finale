using System.Text.Json.Serialization;

namespace ShopMuseoProgettoFinale.Models {
    public class Like {
        public string UserId { get; set; }

        [JsonIgnore]
        public ApplicationUser? ApplicationUser { get; set; }
        public int ProductId { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}