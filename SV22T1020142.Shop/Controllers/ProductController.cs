using Microsoft.AspNetCore.Mvc;
using SV22T1020142.BusinessLayers;
using SV22T1020142.Models.Catalog;
using SV22T1020142.Models.Common;
using SV22T1020142.Shop.Models;

namespace SV22T1020142.Shop.Controllers
{
    public class ProductController : Controller
    {
        public async Task<IActionResult> Index(
            string searchValue = "",
            int categoryID = 0,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int page = 1)
        {
            var categoryTask = CatalogDataService.ListCategoriesAsync(
                new PaginationSearchInput
                {
                    Page = 1,
                    PageSize = 100,
                    SearchValue = ""
                });

            var productTask = CatalogDataService.ListProductsAsync(
                new ProductSearchInput
                {
                    Page = page,
                    PageSize = 12,
                    SearchValue = searchValue ?? "",
                    CategoryID = categoryID,
                    SupplierID = 0,
                    MinPrice = minPrice ?? 0,
                    MaxPrice = maxPrice ?? 0
                });

            await Task.WhenAll(categoryTask, productTask);

            var model = new ProductListViewModel
            {
                Products = await productTask,
                Categories = (await categoryTask).DataItems,
                SearchValue = searchValue ?? "",
                CategoryID = categoryID,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            Product? product = await CatalogDataService.GetProductAsync(id);
            if (product == null)
            {
                TempData["Error"] = "Sản phẩm không tồn tại.";
                return RedirectToAction(nameof(Index));
            }

            Task<List<ProductPhoto>> photosTask = CatalogDataService.ListPhotosAsync(id);
            Task<List<ProductAttribute>> attributesTask = CatalogDataService.ListAttributesAsync(id);
            Task<string> categoryTask = GetCategoryNameAsync(product.CategoryID);
            Task<string> supplierTask = GetSupplierNameAsync(product.SupplierID);

            await Task.WhenAll(photosTask, attributesTask, categoryTask, supplierTask);

            var model = new ShopProductDetailsViewModel
            {
                Product = product,
                Photos = await photosTask,
                Attributes = await attributesTask,
                CategoryName = await categoryTask,
                SupplierName = await supplierTask
            };

            return View(model);
        }

        private static async Task<string> GetCategoryNameAsync(int? categoryID)
        {
            if (!categoryID.HasValue || categoryID.Value <= 0)
                return "";

            var category = await CatalogDataService.GetCategoryAsync(categoryID.Value);
            return category?.CategoryName ?? "";
        }

        private static async Task<string> GetSupplierNameAsync(int? supplierID)
        {
            if (!supplierID.HasValue || supplierID.Value <= 0)
                return "";

            var supplier = await PartnerDataService.GetSupplierAsync(supplierID.Value);
            return supplier?.SupplierName ?? "";
        }
    }
}
