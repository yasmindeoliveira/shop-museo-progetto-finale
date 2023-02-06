using Microsoft.AspNetCore.Identity;

namespace ShopMuseoProgettoFinale.Models {
    public class ApplicationUser : IdentityUser {
        public List<Like>? Likes { get; set; } = new();
    }
}