using Microsoft.AspNetCore.Mvc;
using SV22T1020142.Models.Sales;
using SV22T1020142.Shop.AppCodes;
using SV22T1020142.Shop.Filters;
using SV22T1020142.Shop.Models;
using SV22T1020142.Shop.Services;

namespace SV22T1020142.Shop.Controllers
{
    [CustomerAuthorize]
    public class OrdersController : Controller
    {
        public async Task<IActionResult> MyOrders(int page = 1, int status = 0)
        {
            var customer = CustomerSessionHelper.GetLoggedInCustomer(HttpContext)!;
            var input = new OrderSearchInput
            {
                Page = page,
                PageSize = 20,
                SearchValue = "",
                Status = status,
                FromTime = null,
                ToTime = null,
                CustomerID = customer.CustomerID
            };

            var model = new MyOrdersViewModel
            {
                Orders = await ShopOrderDataService.ListCustomerOrdersAsync(input),
                Status = status
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var customer = CustomerSessionHelper.GetLoggedInCustomer(HttpContext)!;
            var order = await ShopOrderDataService.GetCustomerOrderAsync(id, customer.CustomerID);
            if (order == null)
            {
                TempData["Error"] = "Không tìm thấy đơn hàng phù hợp.";
                return RedirectToAction(nameof(MyOrders));
            }

            var model = new OrderDetailsViewModel
            {
                Order = order,
                Details = await ShopOrderDataService.ListOrderDetailsAsync(id)
            };

            return View(model);
        }
    }
}
