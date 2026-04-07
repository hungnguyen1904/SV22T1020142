using Microsoft.AspNetCore.Mvc;
using SV22T1020142.BusinessLayers;
using SV22T1020142.Models.Catalog;
using SV22T1020142.Models.Common;

namespace SV22T1020142.Admin.Controllers
{
    public class ProductController : Controller
    {
        private const int PAGESIZE = 10;
        private const string PRODUCT_SEARCH = "ProductSearchInput";

        // Trang chính
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Quản lý mặt hàng";

            var input = ApplicationContext.GetSessionData<ProductSearchInput>(PRODUCT_SEARCH);

            if (input == null)
            {
                input = new ProductSearchInput()
                {
                    Page = 1,
                    PageSize = ApplicationContext.PageSize,
                };
            }

            ViewBag.Categories = await CatalogDataService.ListCategoriesAsync(
                new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = ApplicationContext.PageSize,
                });

            return View(input);
        }

        // Tìm kiếm
        public async Task<IActionResult> Search(ProductSearchInput input)
        {
            var result = await CatalogDataService.ListProductsAsync(input);

            ApplicationContext.SetSessionData(PRODUCT_SEARCH, input);

            return View(result);
        }

        // Thêm
        public async Task<IActionResult> Create()
        {
            ViewBag.Title = "Thêm mặt hàng";

            ViewBag.Categories = await CatalogDataService.ListCategoriesAsync(
                new PaginationSearchInput() { Page = 1, PageSize = 100 });

            ViewBag.Suppliers = await PartnerDataService.ListSuppliersAsync(
                new PaginationSearchInput() { Page = 1, PageSize = 100 });

            return View("Edit", new Product());
        }

        // Sửa
        public async Task<IActionResult> Edit(int id)
        {
            var data = await CatalogDataService.GetProductAsync(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật mặt hàng";

            ViewBag.Categories = await CatalogDataService.ListCategoriesAsync(
                new PaginationSearchInput() { Page = 1, PageSize = 100 });

            ViewBag.Suppliers = await PartnerDataService.ListSuppliersAsync(
                new PaginationSearchInput() { Page = 1, PageSize = 100 });

            return View(data);
        }

        // Lưu
        [HttpPost]
        public async Task<IActionResult> Save(Product model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await CatalogDataService.ListCategoriesAsync(
                    new PaginationSearchInput() { Page = 1, PageSize = 100 });

                ViewBag.Suppliers = await PartnerDataService.ListSuppliersAsync(
                    new PaginationSearchInput() { Page = 1, PageSize = 100 });

                return View("Edit", model);
            }

            if (model.ProductID == 0)
                await CatalogDataService.AddProductAsync(model);
            else
                await CatalogDataService.UpdateProductAsync(model);

            return RedirectToAction("Index");
        }

        // Xóa
        public async Task<IActionResult> Delete(int id)
        {
            var data = await CatalogDataService.GetProductAsync(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Xóa mặt hàng";
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product model)
        {
            await CatalogDataService.DeleteProductAsync(model.ProductID);
            return RedirectToAction("Index");
        }
    }
}