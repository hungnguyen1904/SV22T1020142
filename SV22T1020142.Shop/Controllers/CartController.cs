using Microsoft.AspNetCore.Mvc;
using SV22T1020142.BusinessLayers;
using SV22T1020142.Models.Sales;
using SV22T1020142.Shop.AppCodes;
using SV22T1020142.Shop.Filters;
using SV22T1020142.Shop.Models;
using SV22T1020142.Shop.Services;

namespace SV22T1020142.Shop.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var model = new CartViewModel
            {
                Items = CartHelper.GetCart(HttpContext)
            };
            return View(model);
        }

        public async Task<IActionResult> Add(int id, int quantity = 1, string returnUrl = "")
        {
            var product = await CatalogDataService.GetProductAsync(id);
            if (product == null)
            {
                TempData["Error"] = "Sản phẩm không tồn tại.";
                return RedirectToAction("Index", "Product");
            }

            CartHelper.AddToCart(HttpContext, product, quantity <= 0 ? 1 : quantity);
            TempData["Success"] = "Đã thêm vào giỏ hàng.";

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Update(int productID, int quantity)
        {
            CartHelper.UpdateQuantity(HttpContext, productID, quantity);
            TempData["Success"] = "Đã cập nhật giỏ hàng.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            CartHelper.RemoveFromCart(HttpContext, id);
            TempData["Success"] = "Đã xóa sản phẩm khỏi giỏ hàng.";
            return RedirectToAction(nameof(Index));
        }

        [CustomerAuthorize]
        public IActionResult Checkout()
        {
            var cart = CartHelper.GetCart(HttpContext);
            if (!cart.Any())
            {
                TempData["Error"] = "Giỏ hàng đang trống.";
                return RedirectToAction(nameof(Index));
            }

            var customer = CustomerSessionHelper.GetLoggedInCustomer(HttpContext)!;
            var model = new CheckoutViewModel
            {
                CustomerName = customer.CustomerName,
                Phone = customer.Phone,
                DeliveryProvince = customer.Province,
                DeliveryAddress = customer.Address,
                Items = cart
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomerAuthorize]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            var cart = CartHelper.GetCart(HttpContext);
            var customer = CustomerSessionHelper.GetLoggedInCustomer(HttpContext)!;

            if (!cart.Any())
            {
                TempData["Error"] = "Giỏ hàng đang trống.";
                return RedirectToAction(nameof(Index));
            }

            model.Items = cart;
            if (!ModelState.IsValid)
                return View(model);

            await ShopCustomerDataService.SyncCheckoutProfileAsync(customer.CustomerID, model);

            var order = new Order
            {
                CustomerID = customer.CustomerID,
                OrderTime = DateTime.Now,
                DeliveryAddress = model.DeliveryAddress,
                DeliveryProvince = model.DeliveryProvince,
                Status = OrderStatusEnum.New
            };

            int orderID = await ShopOrderDataService.CreateOrderAsync(order, cart);

            var refreshedCustomer = await ShopCustomerDataService.GetSessionDataAsync(customer.CustomerID);
            if (refreshedCustomer != null)
                CustomerSessionHelper.SignIn(HttpContext, refreshedCustomer);

            CartHelper.ClearCart(HttpContext);
            TempData["Success"] = "Đặt hàng thành công.";
            return RedirectToAction("Details", "Orders", new { id = orderID });
        }
    }
}
