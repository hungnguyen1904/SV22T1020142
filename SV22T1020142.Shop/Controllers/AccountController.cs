using Microsoft.AspNetCore.Mvc;
using SV22T1020142.Shop.AppCodes;
using SV22T1020142.Shop.Filters;
using SV22T1020142.Shop.Models;
using SV22T1020142.Shop.Services;

namespace SV22T1020142.Shop.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string returnUrl = "")
        {
            if (CustomerSessionHelper.IsLoggedIn(HttpContext))
                return RedirectToAction("Index", "Product");

            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var customer = await ShopCustomerDataService.AuthorizeAsync(model.Email, model.Password);
            if (customer == null)
            {
                ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng, hoặc tài khoản đang bị khóa.");
                return View(model);
            }

            CustomerSessionHelper.SignIn(HttpContext, customer);
            TempData["Success"] = "Đăng nhập thành công.";

            if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            return RedirectToAction("Index", "Product");
        }

        public IActionResult Register()
        {
            if (CustomerSessionHelper.IsLoggedIn(HttpContext))
                return RedirectToAction("Index", "Product");

            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await ShopCustomerDataService.RegisterAsync(model);
            if (!result.Success || result.Customer == null)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View(model);
            }

            CustomerSessionHelper.SignIn(HttpContext, result.Customer);
            TempData["Success"] = "Đăng ký tài khoản thành công.";
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Logout()
        {
            CustomerSessionHelper.SignOut(HttpContext);
            TempData["Success"] = "Bạn đã đăng xuất.";
            return RedirectToAction("Index", "Product");
        }

        [CustomerAuthorize]
        public async Task<IActionResult> Profile()
        {
            var customer = CustomerSessionHelper.GetLoggedInCustomer(HttpContext)!;
            var model = await ShopCustomerDataService.GetProfileAsync(customer.CustomerID);
            if (model == null)
            {
                TempData["Error"] = "Không tìm thấy thông tin tài khoản.";
                return RedirectToAction("Index", "Product");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomerAuthorize]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            var customer = CustomerSessionHelper.GetLoggedInCustomer(HttpContext)!;
            model.CustomerID = customer.CustomerID;

            if (!ModelState.IsValid)
                return View(model);

            bool updated = await ShopCustomerDataService.UpdateProfileAsync(model);
            if (!updated)
            {
                ModelState.AddModelError(nameof(model.Email), "Email này đang được sử dụng bởi tài khoản khác.");
                return View(model);
            }

            var refreshedCustomer = await ShopCustomerDataService.GetSessionDataAsync(customer.CustomerID);
            if (refreshedCustomer != null)
                CustomerSessionHelper.SignIn(HttpContext, refreshedCustomer);

            TempData["Success"] = "Đã cập nhật thông tin cá nhân.";
            return RedirectToAction(nameof(Profile));
        }

        [CustomerAuthorize]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomerAuthorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var customer = CustomerSessionHelper.GetLoggedInCustomer(HttpContext)!;
            var result = await ShopCustomerDataService.ChangePasswordAsync(
                customer.CustomerID,
                model.CurrentPassword,
                model.NewPassword);

            if (!result.Success)
            {
                ModelState.AddModelError(nameof(model.CurrentPassword), result.ErrorMessage);
                return View(model);
            }

            TempData["Success"] = "Đã đổi mật khẩu thành công.";
            return RedirectToAction(nameof(Profile));
        }
    }
}
