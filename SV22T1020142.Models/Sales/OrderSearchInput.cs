using SV22T1020142.Models.Common;

namespace SV22T1020142.Models.Sales
{
    /// <summary>
    /// Điều kiện tìm kiếm đơn hàng
    /// </summary>
    public class OrderSearchInput : PaginationSearchInput
    {
        /// <summary>
        /// Trạng thái đơn hàng (0 = tất cả)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Thời gian từ ngày
        /// </summary>
        public DateTime? FromTime { get; set; }

        /// <summary>
        /// Thời gian đến ngày
        /// </summary>
        public DateTime? ToTime { get; set; }

        /// <summary>
        /// Mã khách hàng (nếu cần lọc)
        /// </summary>
        public int? CustomerID { get; set; }
    }
}