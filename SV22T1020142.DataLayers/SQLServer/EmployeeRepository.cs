using Dapper;
using Microsoft.Data.SqlClient;
using SV22T1020142.DataLayers.Interfaces;
using SV22T1020142.Models.Common;
using SV22T1020142.Models.HR;

namespace SV22T1020142.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt các chức năng xử lý dữ liệu cho bảng Employees
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Chuỗi kết nối CSDL</param>
        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Lấy danh sách nhân viên có phân trang
        /// </summary>
        public async Task<PagedResult<Employee>> ListAsync(PaginationSearchInput input)
        {
            using var connection = new SqlConnection(_connectionString);

            var result = new PagedResult<Employee>()
            {
                Page = input.Page,
                PageSize = input.PageSize
            };

            string countSql = @"SELECT COUNT(*)
                                FROM Employees
                                WHERE FullName LIKE @SearchValue
                                   OR Phone LIKE @SearchValue
                                   OR Email LIKE @SearchValue";

            result.RowCount = await connection.ExecuteScalarAsync<int>(
                countSql,
                new { SearchValue = $"%{input.SearchValue}%" });

            if (result.RowCount == 0)
                return result;

            string dataSql = @"SELECT *
                               FROM Employees
                               WHERE FullName LIKE @SearchValue
                                  OR Phone LIKE @SearchValue
                                  OR Email LIKE @SearchValue
                               ORDER BY FullName
                               OFFSET @Offset ROWS
                               FETCH NEXT @PageSize ROWS ONLY";

            var data = await connection.QueryAsync<Employee>(
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
        /// Lấy thông tin một nhân viên theo ID
        /// </summary>
        public async Task<Employee?> GetAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"SELECT *
                           FROM Employees
                           WHERE EmployeeID = @id";

            return await connection.QueryFirstOrDefaultAsync<Employee>(sql, new { id });
        }

        /// <summary>
        /// Thêm nhân viên mới
        /// </summary>
        public async Task<int> AddAsync(Employee data)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"INSERT INTO Employees
                           (FullName, BirthDate, Address, Phone, Email, Photo, IsWorking)
                           VALUES
                           (@FullName, @BirthDate, @Address, @Phone, @Email, @Photo, @IsWorking);
                           SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await connection.ExecuteScalarAsync<int>(sql, data);
        }

        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        public async Task<bool> UpdateAsync(Employee data)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"UPDATE Employees
                           SET FullName = @FullName,
                               BirthDate = @BirthDate,
                               Address = @Address,
                               Phone = @Phone,
                               Email = @Email,
                               Photo = @Photo,
                               IsWorking = @IsWorking
                           WHERE EmployeeID = @EmployeeID";

            int rows = await connection.ExecuteAsync(sql, data);
            return rows > 0;
        }

        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"DELETE FROM Employees
                           WHERE EmployeeID = @id";

            int rows = await connection.ExecuteAsync(sql, new { id });
            return rows > 0;
        }

        /// <summary>
        /// Kiểm tra nhân viên có dữ liệu liên quan hay không
        /// </summary>
        public async Task<bool> IsUsedAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"SELECT COUNT(*)
                           FROM Orders
                           WHERE EmployeeID = @id";

            int count = await connection.ExecuteScalarAsync<int>(sql, new { id });
            return count > 0;
        }

        /// <summary>
        /// Kiểm tra email của nhân viên có hợp lệ (không bị trùng) hay không
        /// </summary>
        public async Task<bool> ValidateEmailAsync(string email, int id = 0)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql;

            if (id == 0)
            {
                sql = @"SELECT COUNT(*)
                        FROM Employees
                        WHERE Email = @email";
                int count = await connection.ExecuteScalarAsync<int>(sql, new { email });
                return count == 0;
            }
            else
            {
                sql = @"SELECT COUNT(*)
                        FROM Employees
                        WHERE Email = @email AND EmployeeID <> @id";
                int count = await connection.ExecuteScalarAsync<int>(sql, new { email, id });
                return count == 0;
            }
        }
    }
}