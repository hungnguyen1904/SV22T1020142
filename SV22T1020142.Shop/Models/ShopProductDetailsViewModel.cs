using SV22T1020142.Models.Catalog;

namespace SV22T1020142.Shop.Models
{
    public class ShopProductDetailsViewModel
    {
        public Product? Product { get; set; }
        public List<ProductPhoto> Photos { get; set; } = new();
        public List<ProductAttribute> Attributes { get; set; } = new();
        public string CategoryName { get; set; } = "";
        public string SupplierName { get; set; } = "";
    }
}
