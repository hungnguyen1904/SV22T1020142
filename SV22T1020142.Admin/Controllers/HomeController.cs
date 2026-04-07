using Microsoft.AspNetCore.Mvc;
using SV22T1020142.Admin.Models;
using System.Diagnostics;

/// <summary>
/// Controller xử lý các chức năng chính tại trang chủ của hệ thống.
/// Bao gồm:
/// - Trang chủ (Index)
/// - Trang chính sách riêng tư (Privacy)
/// - Trang hiển thị lỗi (Error)
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// Đối tượng ghi log cho HomeController.
    /// </summary>
    private readonly ILogger<HomeController> _logger;

    /// <summary>
    /// Hàm khởi tạo, tiêm ILogger để ghi log hệ thống.
    /// </summary>
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Hiển thị trang chủ của hệ thống.
    /// </summary>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Hiển thị trang chính sách bảo mật.
    /// </summary>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Hiển thị trang lỗi.
    /// Không lưu cache để đảm bảo luôn hiển thị đúng thông tin lỗi mới nhất.
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}