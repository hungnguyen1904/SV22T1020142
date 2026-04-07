using Microsoft.AspNetCore.Mvc;

namespace SV22T1020142.Admin.Controllers
{
    /// <summary>
    /// Các chức năng liên quan đến nghiệp vụ bán hàng
    /// </summary>
    public class OrderController : Controller
    {
        /// <summary>
        /// Giao diện nhập đầu vào tìm kiếm đơn hàng và hiển thị kết quà tìm kiếm
        /// </summary>
        public IActionResult Index()
        {
            ViewBag.Title = "Quản lý đơn hàng";
            return View();
        }

        /// <summary>
        /// Tìm kiếm đơn hàng theo từ khóa.
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm (mã đơn, tên khách hàng,...).</param>
        public IActionResult Search(string keyword)
        {
            ViewBag.Title = "Tìm kiếm đơn hàng";
            ViewBag.Keyword = keyword;
            return View();
        }

        /// <summary>
        /// Hiển thị chi tiết một đơn hàng và các chức năng xử lý khác
        /// </summary>
        /// <param name="id">Mã đơn hàng cần xem chi tiết.</param>
        public IActionResult Detail(int id)
        {
            ViewBag.Title = "Chi tiết đơn hàng";
            return View();
        }

        /// <summary>
        /// Giao diện cung cấp các chức năng nghiệp vụ lập đơn hàng mới.
        /// </summary>
        public IActionResult Create()
        {
            ViewBag.Title = "Lập đơn hàng";
            return View();
        }

        /// <summary>
        /// Hiển thị trang xác nhận xóa đơn hàng.
        /// </summary>
        /// <param name="id">Mã đơn hàng cần xóa.</param>
        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Xóa đơn hàng";
            return View();
        }

        /// <summary>
        /// Cập nhật thông tin (số lượng, giá bán) của một mặt hàng
        /// trong giỏ hàng hoặc trong một đơn hàng
        /// </summary>
        /// <param name="id"> bằng 0: cập nhật giỏ hàng, 
        /// khác 0: cập nhật cho đơn hàng có mã id.</param>
        /// <param name="productId">Mã sản phẩm cần chỉnh sửa số lượng hoặc giá bán.</param>
        public IActionResult EditCartItem(int id, int productId)
        {
            ViewBag.Title = "Cập nhật sản phẩm trong giỏ hàng";
            ViewBag.OrderId = id;
            ViewBag.ProductId = productId;
            return View();
        }

        /// <summary>
        /// Xóa một sản phẩm khỏi giỏ hàng hoặc đơn hàng.
        /// </summary>
        /// <param name="id">0: xóa khỏi giỏ hàng, 
        /// khác 0: xóa khỏi cho đơn hàng có mã id.</param></param>
        /// <param name="productId">Mã sản phẩm cần xóa.</param>
        public IActionResult DeleteCartItem(int id, int productId)
        {
            return RedirectToAction("Create");
        }

        /// <summary>
        /// Xóa toàn bộ sản phẩm trong giỏ hàng hiện tại.
        /// </summary>
        public IActionResult ClearCart()
        {
            return RedirectToAction("Create");
        }

        /// <summary>
        /// Duyệt đơn hàng (cập nhật trạng thái thành "Đã duyệt").
        /// </summary>
        /// <param name="id">Mã đơn hàng cần chấp nhận.</param>
        public IActionResult Accept(int id)
        {
            return RedirectToAction("Detail", new { id });
        }

        /// <summary>
        /// Cập nhật trạng thái đơn hàng thành "Đang giao".
        /// </summary>
        /// <param name="id">Mã đơn hàng đang giao.</param>
        public IActionResult Shipping(int id)
        {
            return RedirectToAction("Detail", new { id });
        }

        /// <summary>
        /// Cập nhật trạng thái đơn hàng thành "Hoàn thành".
        /// </summary>
        /// <param name="id">Mã đơn hàng hoàn thành.</param>
        public IActionResult Finish(int id)
        {
            return RedirectToAction("Detail", new { id });
        }

        /// <summary>
        /// Cập nhật trạng thái đơn hàng thành "Từ chối".
        /// </summary>
        /// <param name="id">Mã đơn hàng cần từ chối.</param>
        public IActionResult Reject(int id)
        {
            return RedirectToAction("Detail", new { id });
        }

        /// <summary>
        /// Cập nhật trạng thái đơn hàng thành "Hủy".
        /// </summary>
        /// <param name="id">Mã đơn hàng cần hủy.</param>
        public IActionResult Cancel(int id)
        {
            return RedirectToAction("Detail", new { id });
        }
    }
}