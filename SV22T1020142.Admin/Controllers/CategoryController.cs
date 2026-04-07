using Microsoft.AspNetCore.Mvc;
using SV22T1020142.BusinessLayers;
using SV22T1020142.Models.Catalog;
using SV22T1020142.Models.Common;

namespace SV22T1020142.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private const int PAGESIZE = 10;
        private const string CATEGORY_SEARCH = "CategorySearchInput";

        // Trang chính
        public IActionResult Index()
        {
            var input = ApplicationContext.GetSessionData<PaginationSearchInput>(CATEGORY_SEARCH);

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
            var result = await CatalogDataService.ListCategoriesAsync(input);

            ApplicationContext.SetSessionData(CATEGORY_SEARCH, input);

            return View(result);
        }

        // Thêm mới
        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung loại hàng";
            return View("Edit", new Category());
        }

        // Chỉnh sửa
        public async Task<IActionResult> Edit(int id)
        {
            var data = await CatalogDataService.GetCategoryAsync(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật loại hàng";
            return View(data);
        }

        // Lưu dữ liệu
        [HttpPost]
        public async Task<IActionResult> Save(Category data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.CategoryName))
                    ModelState.AddModelError(nameof(data.CategoryName), "Vui lòng nhập tên loại hàng");

                if (!ModelState.IsValid)
                    return View("Edit", data);

                if (data.CategoryID == 0)
                    await CatalogDataService.AddCategoryAsync(data);
                else
                    await CatalogDataService.UpdateCategoryAsync(data);

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
                await CatalogDataService.DeleteCategoryAsync(id);
                return RedirectToAction("Index");
            }

            var data = await CatalogDataService.GetCategoryAsync(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Xóa loại hàng";
            ViewBag.CanDelete = !(await CatalogDataService.IsUsedCategoryAsync(id));

            return View(data);
        }
    }
}