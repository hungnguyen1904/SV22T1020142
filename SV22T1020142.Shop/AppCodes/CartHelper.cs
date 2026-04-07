using SV22T1020142.Models.Catalog;

namespace SV22T1020142.Shop.AppCodes
{
    public static class CartHelper
    {
        private const string CART_KEY = "SHOPPING_CART";

        public static List<CartItem> GetCart(HttpContext context)
        {
            return context.Session.GetSessionData<List<CartItem>>(CART_KEY) ?? new List<CartItem>();
        }

        public static List<CartItem> GetCart(ISession session)
        {
            return session.GetSessionData<List<CartItem>>(CART_KEY) ?? new List<CartItem>();
        }

        public static void SaveCart(HttpContext context, List<CartItem> cart)
        {
            context.Session.SetSessionData(CART_KEY, cart);
        }

        public static void AddToCart(HttpContext context, Product product, int quantity = 1)
        {
            var cart = GetCart(context);
            var item = cart.FirstOrDefault(x => x.Product.ProductID == product.ProductID);

            if (item == null)
            {
                cart.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                item.Quantity += quantity;
            }

            SaveCart(context, cart);
        }

        public static void UpdateQuantity(HttpContext context, int productID, int quantity)
        {
            var cart = GetCart(context);
            var item = cart.FirstOrDefault(x => x.Product.ProductID == productID);

            if (item != null)
            {
                if (quantity <= 0)
                    cart.Remove(item);
                else
                    item.Quantity = quantity;
            }

            SaveCart(context, cart);
        }

        public static void RemoveFromCart(HttpContext context, int productID)
        {
            var cart = GetCart(context);
            cart.RemoveAll(x => x.Product.ProductID == productID);
            SaveCart(context, cart);
        }

        public static void ClearCart(HttpContext context)
        {
            SaveCart(context, new List<CartItem>());
        }

        public static dynamic GetCartSummary(ISession session)
        {
            var cart = GetCart(session);
            return new
            {
                Count = cart.Sum(x => x.Quantity),
                Total = cart.Sum(x => x.TotalPrice)
            };
        }
    }
}