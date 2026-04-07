using Microsoft.AspNetCore.Mvc;
using SV22T1020142.BusinessLayers;
using SV22T1020142.Models.Common;
using SV22T1020142.Models.Partner;

namespace SV22T1020142.Admin.Controllers
{
    public class SupplierController : Controller
    {
        private const int PAGESIZE = 10;
        private const string SUPPLIER_SEARCH = "SupplierSearchInput";

        // Trang chính
        public IActionResult Index()
        {
            var input = ApplicationContext.GetSessionData<PaginationSearchInput>(SUPPLIER_SEARCH);

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
            var result = await PartnerDataService.ListSuppliersAsync(input);

            ApplicationContext.SetSessionData(SUPPLIER_SEARCH, input);

            return View(result);
        }

        // Tạo mới
        public async Task<IActionResult> Create()
        {
            ViewBag.Title = "Bổ sung nhà cung cấp";
            ViewBag.Provinces = await DictionaryDataService.ListProvincesAsync();
            return View("Edit", new Supplier());
        }

        // Chỉnh sửa
        public async Task<IActionResult> Edit(int id)
        {
            var data = await PartnerDataService.GetSupplierAsync(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật nhà cung cấp";
            ViewBag.Provinces = await DictionaryDataService.ListProvincesAsync();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Supplier data)
        {
            try
            {
                ViewBag.Provinces = await DictionaryDataService.ListProvincesAsync();

                if (string.IsNullOrWhiteSpace(data.SupplierName))
                    ModelState.AddModelError(nameof(data.SupplierName), "Vui lòng nhập tên nhà cung cấp");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError(nameof(data.ContactName), "Vui lòng nhập tên giao dịch");
                if (string.IsNullOrWhiteSpace(data.Phone))
                    ModelState.AddModelError(nameof(data.Phone), "Vui lòng nhập số điện thoại");
                if (string.IsNullOrWhiteSpace(data.Email))
                    ModelState.AddModelError(nameof(data.Email), "Vui lòng nhập email");
                if (string.IsNullOrWhiteSpace(data.Address))
                    ModelState.AddModelError(nameof(data.Address), "Vui lòng nhập địa chỉ");
                if (string.IsNullOrWhiteSpace(data.Province))
                    ModelState.AddModelError(nameof(data.Province), "Vui lòng chọn tỉnh/thành");

                if (!ModelState.IsValid)
                    return View("Edit", data);

                if (data.SupplierID == 0)
                    await PartnerDataService.AddSupplierAsync(data);
                else
                    await PartnerDataService.UpdateSupplierAsync(data);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Hệ thống đang lỗi vui lòng thử lại sau");
                ViewBag.Provinces = await DictionaryDataService.ListProvincesAsync();
                return View("Edit", data);
            }
        }

        // Xóa
        public async Task<IActionResult> Delete(int id)
        {
            if (Request.Method == "POST")
            {
                await PartnerDataService.DeleteSupplierAsync(id);
                return RedirectToAction("Index");
            }

            var data = await PartnerDataService.GetSupplierAsync(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Xóa nhà cung cấp";
            ViewBag.CanDelete = !(await PartnerDataService.IsUsedSupplierAsync(id));

            return View(data);
        }
    }
}