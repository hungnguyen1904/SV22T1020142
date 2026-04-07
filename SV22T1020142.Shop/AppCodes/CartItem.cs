using SV22T1020142.Models.Catalog;

namespace SV22T1020142.Shop.AppCodes
{
    public class CartItem
    {
        public Product Product { get; set; } = new Product();
        public int Quantity { get; set; }

        public decimal TotalPrice => Product.Price * Quantity;
    }
}