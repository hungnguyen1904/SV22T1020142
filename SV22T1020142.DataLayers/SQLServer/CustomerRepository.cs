using Dapper;
using Microsoft.Data.SqlClient;
using SV22T1020142.DataLayers.Interfaces;
using SV22T1020142.Models.Common;
using SV22T1020142.Models.Partner;

namespace SV22T1020142.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt các chức năng xử lý dữ liệu cho bảng Customers
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Chuỗi kết nối CSDL</param>
        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Lấy danh sách khách hàng có phân trang
        /// </summary>
        public async Task<PagedResult<Customer>> ListAsync(PaginationSearchInput input)
        {
            using var connection = new SqlConnection(_connectionString);

            var result = new PagedResult<Customer>()
            {
                Page = input.Page,
                PageSize = input.PageSize
            };

            string countSql = @"SELECT COUNT(*)
                                FROM Customers
                                WHERE CustomerName LIKE @SearchValue
                                   OR ContactName LIKE @SearchValue
                                   OR Phone LIKE @SearchValue";

            result.RowCount = await connection.ExecuteScalarAsync<int>(
                countSql,
                new { SearchValue = $"%{input.SearchValue}%" });

            if (result.RowCount == 0)
                return result;

            string dataSql = @"SELECT *
                               FROM Customers
                               WHERE CustomerName LIKE @SearchValue
                                  OR ContactName LIKE @SearchValue
                                  OR Phone LIKE @SearchValue
                               ORDER BY CustomerName
                               OFFSET @Offset ROWS
                               FETCH NEXT @PageSize ROWS ONLY";

            var data = await connection.QueryAsync<Customer>(
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
        /// Lấy thông tin khách hàng theo ID
        /// </summary>
        public async Task<Customer?> GetAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"SELECT *
                           FROM Customers
                           WHERE CustomerID = @id";

            return await connection.QueryFirstOrDefaultAsync<Customer>(sql, new { id });
        }

        /// <summary>
        /// Thêm khách hàng mới
        /// </summary>
        public async Task<int> AddAsync(Customer data)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"INSERT INTO Customers
                           (CustomerName, ContactName, Province, Address, Phone, Email, IsLocked)
                           VALUES
                           (@CustomerName, @ContactName, @Province, @Address, @Phone, @Email, @IsLocked);
                           SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await connection.ExecuteScalarAsync<int>(sql, data);
        }

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        public async Task<bool> UpdateAsync(Customer data)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"UPDATE Customers
                           SET CustomerName = @CustomerName,
                               ContactName = @ContactName,
                               Province = @Province,
                               Address = @Address,
                               Phone = @Phone,
                               Email = @Email,
                               IsLocked = @IsLocked
                           WHERE CustomerID = @CustomerID";

            int rows = await connection.ExecuteAsync(sql, data);
            return rows > 0;
        }

        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"DELETE FROM Customers
                           WHERE CustomerID = @id";

            int rows = await connection.ExecuteAsync(sql, new { id });
            return rows > 0;
        }

        /// <summary>
        /// Kiểm tra khách hàng có dữ liệu liên quan (đơn hàng) hay không
        /// </summary>
        public async Task<bool> IsUsedAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"SELECT COUNT(*)
                           FROM Orders
                           WHERE CustomerID = @id";

            int count = await connection.ExecuteScalarAsync<int>(sql, new { id });
            return count > 0;
        }

        /// <summary>
        /// Kiểm tra email có hợp lệ (không bị trùng) hay không
        /// </summary>
        public async Task<bool> ValidateEmailAsync(string email, int id = 0)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql;

            if (id == 0)
            {
                // Kiểm tra email khi thêm mới
                sql = @"SELECT COUNT(*)
                        FROM Customers
                        WHERE Email = @email";
                int count = await connection.ExecuteScalarAsync<int>(sql, new { email });
                return count == 0;
            }
            else
            {
                // Kiểm tra email khi cập nhật
                sql = @"SELECT COUNT(*)
                        FROM Customers
                        WHERE Email = @email AND CustomerID <> @id";
                int count = await connection.ExecuteScalarAsync<int>(sql, new { email, id });
                return count == 0;
            }
        }
    }
}