using Dapper;
using Microsoft.Data.SqlClient;
using SV22T1020142.BusinessLayers;
using SV22T1020142.Models.Common;
using SV22T1020142.Models.Sales;
using SV22T1020142.Shop.AppCodes;

namespace SV22T1020142.Shop.Services
{
    public static class ShopOrderDataService
    {
        public static async Task<int> CreateOrderAsync(Order order, IEnumerable<CartItem> items)
        {
            using var connection = new SqlConnection(Configuration.ConnectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                const string orderSql = @"INSERT INTO Orders
                                          (CustomerID, OrderTime, DeliveryProvince, DeliveryAddress, EmployeeID, AcceptTime, ShipperID, ShippedTime, FinishedTime, Status)
                                          VALUES
                                          (@CustomerID, @OrderTime, @DeliveryProvince, @DeliveryAddress, @EmployeeID, @AcceptTime, @ShipperID, @ShippedTime, @FinishedTime, @Status);
                                          SELECT CAST(SCOPE_IDENTITY() AS INT);";

                int orderID = await connection.ExecuteScalarAsync<int>(orderSql, order, transaction);

                const string detailSql = @"INSERT INTO OrderDetails
                                           (OrderID, ProductID, Quantity, SalePrice)
                                           VALUES
                                           (@OrderID, @ProductID, @Quantity, @SalePrice)";

                foreach (var item in items)
                {
                    await connection.ExecuteAsync(
                        detailSql,
                        new
                        {
                            OrderID = orderID,
                            ProductID = item.Product.ProductID,
                            item.Quantity,
                            SalePrice = item.Product.Price
                        },
                        transaction);
                }

                transaction.Commit();
                return orderID;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public static async Task<PagedResult<OrderViewInfo>> ListCustomerOrdersAsync(OrderSearchInput input)
        {
            using var connection = new SqlConnection(Configuration.ConnectionString);

            var result = new PagedResult<OrderViewInfo>
            {
                Page = input.Page,
                PageSize = input.PageSize
            };

            const string conditionSql = @"FROM Orders o
                                          LEFT JOIN Customers c ON o.CustomerID = c.CustomerID
                                          LEFT JOIN Employees e ON o.EmployeeID = e.EmployeeID
                                          LEFT JOIN Shippers s ON o.ShipperID = s.ShipperID
                                          WHERE (@CustomerID IS NULL OR o.CustomerID = @CustomerID)
                                            AND (@Status = 0 OR o.Status = @Status)
                                            AND (@FromTime IS NULL OR o.OrderTime >= @FromTime)
                                            AND (@ToTime IS NULL OR o.OrderTime < DATEADD(DAY, 1, @ToTime))
                                            AND (@SearchValue = '' OR c.CustomerName LIKE @Keyword OR c.ContactName LIKE @Keyword OR CONVERT(nvarchar(20), o.OrderID) LIKE @Keyword)";

            int rowCount = await connection.ExecuteScalarAsync<int>(
                $"SELECT COUNT(*) {conditionSql}",
                new
                {
                    input.CustomerID,
                    input.Status,
                    input.FromTime,
                    input.ToTime,
                    input.SearchValue,
                    Keyword = $"%{input.SearchValue}%"
                });

            result.RowCount = rowCount;
            if (rowCount == 0)
                return result;

            string dataSql = $@"SELECT o.OrderID,
                                       o.CustomerID,
                                       o.OrderTime,
                                       o.DeliveryProvince,
                                       o.DeliveryAddress,
                                       o.EmployeeID,
                                       o.AcceptTime,
                                       o.ShipperID,
                                       o.ShippedTime,
                                       o.FinishedTime,
                                       o.Status,
                                       ISNULL(e.FullName, '') AS EmployeeName,
                                       ISNULL(c.CustomerName, '') AS CustomerName,
                                       ISNULL(c.ContactName, '') AS CustomerContactName,
                                       ISNULL(c.Email, '') AS CustomerEmail,
                                       ISNULL(c.Phone, '') AS CustomerPhone,
                                       ISNULL(c.Address, '') AS CustomerAddress,
                                       ISNULL(s.ShipperName, '') AS ShipperName,
                                       ISNULL(s.Phone, '') AS ShipperPhone
                                {conditionSql}
                                ORDER BY o.OrderTime DESC
                                OFFSET @Offset ROWS
                                FETCH NEXT @PageSize ROWS ONLY";

            var data = await connection.QueryAsync<OrderViewInfo>(
                dataSql,
                new
                {
                    input.CustomerID,
                    input.Status,
                    input.FromTime,
                    input.ToTime,
                    input.SearchValue,
                    Keyword = $"%{input.SearchValue}%",
                    Offset = input.Offset,
                    input.PageSize
                });

            result.DataItems = data.ToList();
            return result;
        }

        public static async Task<OrderViewInfo?> GetCustomerOrderAsync(int orderID, int customerID)
        {
            using var connection = new SqlConnection(Configuration.ConnectionString);

            const string sql = @"SELECT o.OrderID,
                                        o.CustomerID,
                                        o.OrderTime,
                                        o.DeliveryProvince,
                                        o.DeliveryAddress,
                                        o.EmployeeID,
                                        o.AcceptTime,
                                        o.ShipperID,
                                        o.ShippedTime,
                                        o.FinishedTime,
                                        o.Status,
                                        ISNULL(e.FullName, '') AS EmployeeName,
                                        ISNULL(c.CustomerName, '') AS CustomerName,
                                        ISNULL(c.ContactName, '') AS CustomerContactName,
                                        ISNULL(c.Email, '') AS CustomerEmail,
                                        ISNULL(c.Phone, '') AS CustomerPhone,
                                        ISNULL(c.Address, '') AS CustomerAddress,
                                        ISNULL(s.ShipperName, '') AS ShipperName,
                                        ISNULL(s.Phone, '') AS ShipperPhone
                                 FROM Orders o
                                 LEFT JOIN Customers c ON o.CustomerID = c.CustomerID
                                 LEFT JOIN Employees e ON o.EmployeeID = e.EmployeeID
                                 LEFT JOIN Shippers s ON o.ShipperID = s.ShipperID
                                 WHERE o.OrderID = @orderID
                                   AND o.CustomerID = @customerID";

            return await connection.QueryFirstOrDefaultAsync<OrderViewInfo>(sql, new { orderID, customerID });
        }

        public static async Task<List<OrderDetailViewInfo>> ListOrderDetailsAsync(int orderID)
        {
            using var connection = new SqlConnection(Configuration.ConnectionString);

            const string sql = @"SELECT od.OrderID,
                                        od.ProductID,
                                        od.Quantity,
                                        od.SalePrice,
                                        ISNULL(p.ProductName, '') AS ProductName,
                                        ISNULL(p.Unit, '') AS Unit,
                                        ISNULL(p.Photo, '') AS Photo
                                 FROM OrderDetails od
                                 LEFT JOIN Products p ON od.ProductID = p.ProductID
                                 WHERE od.OrderID = @orderID
                                 ORDER BY p.ProductName";

            var data = await connection.QueryAsync<OrderDetailViewInfo>(sql, new { orderID });
            return data.ToList();
        }
    }
}
