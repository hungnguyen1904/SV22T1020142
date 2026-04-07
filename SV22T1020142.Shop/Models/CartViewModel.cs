using SV22T1020142.Shop.AppCodes;

namespace SV22T1020142.Shop.Models
{
    public class CartViewModel
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal TotalAmount => Items.Sum(x => x.TotalPrice);
    }
}