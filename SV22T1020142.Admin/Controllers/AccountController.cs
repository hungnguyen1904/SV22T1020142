using Microsoft.AspNetCore.Mvc;

namespace SV22T1020142.Admin.Controllers
{
    /// <summary>
    /// Quản lý chức năng đăng nhập và tài khoản người dùng.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Hiển thị trang đăng nhập hệ thống.
        /// </summary>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Thực hiện đăng xuất và chuyển về trang phù hợp.
        /// </summary>
        public IActionResult Logout()
        {
            return RedirectToAction();
        }

        /// <summary>
        /// Hiển thị trang đổi mật khẩu của người dùng.
        /// </summary>
        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}