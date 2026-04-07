using SV22T1020142.Models.Common;
using SV22T1020142.Models.Sales;

namespace SV22T1020142.Shop.Models
{
    public class MyOrdersViewModel
    {
        public PagedResult<OrderViewInfo> Orders { get; set; } = new();
        public int Status { get; set; }
    }
}
