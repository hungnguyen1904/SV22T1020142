using Microsoft.AspNetCore.Mvc;
using SV22T1020142.Models.Common;
using SV22T1020142.Models.Partner;

namespace SV22T1020142.Admin.Controllers
{
    public class ShipperController : Controller
    {
        private const int PAGESIZE = 10;
        private const string SHIPPER_SEARCH = "ShipperSearchInput";

        // Trang chính
        public IActionResult Index()
        {
            var input = ApplicationContext.GetSessionData<PaginationSearchInput>(SHIPPER_SEARCH);

            if (input == null)
                input = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = ApplicationContext.PageSize,
                    SearchValue = ""
                };

            return View(input);
        }

        // Tìm kiếm
        public async Task<IActionResult> Search(PaginationSearchInput input)
        {
            var result = await PartnerDataService.ListShippersAsync(input);

            ApplicationContext.SetSessionData(SHIPPER_SEARCH, input);

            return View(result);
        }

        // Thêm mới
        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung người giao hàng";
            return View("Edit", new Shipper());
        }

        // Chỉnh sửa
        public async Task<IActionResult> Edit(int id)
        {
            var data = await PartnerDataService.GetShipperAsync(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật người giao hàng";
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Shipper data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.ShipperName))
                    ModelState.AddModelError(nameof(data.ShipperName), "Vui lòng nhập tên người giao hàng");
                if (string.IsNullOrWhiteSpace(data.Phone))
                    ModelState.AddModelError(nameof(data.Phone), "Vui lòng nhập số điện thoại");

                if (!ModelState.IsValid)
                    return View("Edit", data);

                if (data.ShipperID == 0)
                    await PartnerDataService.AddShipperAsync(data);
                else
                    await PartnerDataService.UpdateShipperAsync(data);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Hệ thống đang lỗi vui lòng thử lại sau");
                return View("Edit", data);
            }
        }

        // Xóa
        public async Task<IActionResult> Delete(int id)
        {
            if (Request.Method == "POST")
            {
                await PartnerDataService.DeleteShipperAsync(id);
                return RedirectToAction("Index");
            }

            var data = await PartnerDataService.GetShipperAsync(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Xóa người giao hàng";
            ViewBag.CanDelete = !(await PartnerDataService.IsUsedShipperAsync(id));

            return View(data);
        }
    }
}