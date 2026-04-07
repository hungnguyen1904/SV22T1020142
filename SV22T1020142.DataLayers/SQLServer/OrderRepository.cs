using Dapper;
using Microsoft.Data.SqlClient;
using SV22T1020142.DataLayers.Interfaces;
using SV22T1020142.Models.Common;
using SV22T1020142.Models.Sales;

namespace SV22T1020142.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt các chức năng xử lý dữ liệu cho bảng Orders
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Chuỗi kết nối CSDL</param>
        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách đơn hàng có phân trang
        /// </summary>
        public async Task<PagedResult<OrderViewInfo>> ListAsync(OrderSearchInput input)
        {
            using var connection = new SqlConnection(_connectionString);

            var result = new PagedResult<OrderViewInfo>()
            {
                Page = input.Page,
                PageSize = input.PageSize
            };

            string countSql = @"SELECT COUNT(*)
                                FROM Orders
                                WHERE CustomerName LIKE @SearchValue";

            result.RowCount = await connection.ExecuteScalarAsync<int>(
                countSql,
                new { SearchValue = $"%{input.SearchValue}%" });

            if (result.RowCount == 0)
                return result;

            string dataSql = @"SELECT *
                               FROM Orders
                               WHERE CustomerName LIKE @SearchValue
                               ORDER BY OrderTime DESC
                               OFFSET @Offset ROWS
                               FETCH NEXT @PageSize ROWS ONLY";

            var data = await connection.QueryAsync<OrderViewInfo>(
                dataSql,
                new
                {
                    SearchValue = $"%{input.SearchValue}%",
                    Offset = input.Offset,
                    PageSize = input.PageSize
                });

            result.DataItems = data.ToList();
            return result;
        }

        /// <summary>
        /// Lấy thông tin một đơn hàng theo ID
        /// </summary>
        public async Task<OrderViewInfo?> GetAsync(int orderID)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"SELECT *
                           FROM Orders
                           WHERE OrderID = @orderID";

            return await connection.QueryFirstOrDefaultAsync<OrderViewInfo>(sql, new { orderID });
        }

        /// <summary>
        /// Thêm đơn hàng mới
        /// </summary>
        public async Task<int> AddAsync(Order data)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"INSERT INTO Orders
                           (CustomerID, EmployeeID, OrderTime, Status, DeliveryAddress)
                           VALUES
                           (@CustomerID, @EmployeeID, @OrderTime, @Status, @DeliveryAddress);
                           SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await connection.ExecuteScalarAsync<int>(sql, data);
        }

        /// <summary>
        /// Cập nhật thông tin đơn hàng
        /// </summary>
        public async Task<bool> UpdateAsync(Order data)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"UPDATE Orders
                           SET CustomerID = @CustomerID,
                               EmployeeID = @EmployeeID,
                               OrderTime = @OrderTime,
                               Status = @Status,
                               DeliveryAddress = @DeliveryAddress
                           WHERE OrderID = @OrderID";

            int rows = await connection.ExecuteAsync(sql, data);
            return rows > 0;
        }

        /// <summary>
        /// Xóa đơn hàng
        /// </summary>
        public async Task<bool> DeleteAsync(int orderID)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"DELETE FROM Orders
                           WHERE OrderID = @orderID";

            int rows = await connection.ExecuteAsync(sql, new { orderID });
            return rows > 0;
        }

        /// <summary>
        /// Lấy danh sách mặt hàng trong đơn hàng
        /// </summary>
        public async Task<List<OrderDetailViewInfo>> ListDetailsAsync(int orderID)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"SELECT *
                           FROM OrderDetails
                           WHERE OrderID = @orderID";

            var data = await connection.QueryAsync<OrderDetailViewInfo>(sql, new { orderID });
            return data.ToList();
        }

        /// <summary>
        /// Lấy thông tin chi tiết một mặt hàng trong đơn hàng
        /// </summary>
        public async Task<OrderDetailViewInfo?> GetDetailAsync(int orderID, int productID)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"SELECT *
                           FROM OrderDetails
                           WHERE OrderID = @orderID AND ProductID = @productID";

            return await connection.QueryFirstOrDefaultAsync<OrderDetailViewInfo>(
                sql,
                new { orderID, productID });
        }

        /// <summary>
        /// Thêm mặt hàng vào đơn hàng
        /// </summary>
        public async Task<bool> AddDetailAsync(OrderDetail data)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"INSERT INTO OrderDetails
                           (OrderID, ProductID, Quantity, SalePrice)
                           VALUES
                           (@OrderID, @ProductID, @Quantity, @SalePrice)";

            int rows = await connection.ExecuteAsync(sql, data);
            return rows > 0;
        }

        /// <summary>
        /// Cập nhật số lượng và giá bán của mặt hàng trong đơn hàng
        /// </summary>
        public async Task<bool> UpdateDetailAsync(OrderDetail data)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"UPDATE OrderDetails
                           SET Quantity = @Quantity,
                               SalePrice = @SalePrice
                           WHERE OrderID = @OrderID AND ProductID = @ProductID";

            int rows = await connection.ExecuteAsync(sql, data);
            return rows > 0;
        }

        /// <summary>
        /// Xóa một mặt hàng khỏi đơn hàng
        /// </summary>
        public async Task<bool> DeleteDetailAsync(int orderID, int productID)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"DELETE FROM OrderDetails
                           WHERE OrderID = @orderID AND ProductID = @productID";

            int rows = await connection.ExecuteAsync(sql, new { orderID, productID });
            return rows > 0;
        }
    }
}