using SV22T1020142.Models.Catalog;
using SV22T1020142.Models.Common;

namespace SV22T1020142.Shop.Models
{
    public class ProductListViewModel
    {
        public PagedResult<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public string SearchValue { get; set; } = "";
        public int CategoryID { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
