using Microsoft.AspNetCore.Identity;

namespace SexyWebSite.Data
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<Product> Products { get; set; } = new();

        public virtual List<ProdutInCartInfo> ProductsOnCart { get; set; } = new();
    }
}
