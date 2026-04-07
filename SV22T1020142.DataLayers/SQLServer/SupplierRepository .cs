using Dapper;
using Microsoft.Data.SqlClient;
using SV22T1020142.DataLayers.Interfaces;
using SV22T1020142.Models.Common;
using SV22T1020142.Models.Partner;

namespace SV22T1020142.DataLayers.SQLServer
{
    public class SupplierRepository : IGenericRepository<Supplier>
    {
        private readonly string _connectionString;

        /// <summary>
        /// Constructor nhận chuỗi kết nối CSDL
        /// </summary>
        public SupplierRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<PagedResult<Supplier>> ListAsync(PaginationSearchInput input)
        {
            using var connection = new SqlConnection(_connectionString);

            var result = new PagedResult<Supplier>()
            {
                Page = input.Page,
                PageSize = input.PageSize
            };

            string countSql = @"SELECT COUNT(*)
                                FROM Suppliers
                                WHERE SupplierName LIKE @SearchValue";

            result.RowCount = await connection.ExecuteScalarAsync<int>(
                countSql,
                new { SearchValue = $"%{input.SearchValue}%" });

            if (result.RowCount == 0)
                return result;

            string dataSql = @"SELECT *
                               FROM Suppliers
                               WHERE SupplierName LIKE @SearchValue
                               ORDER BY SupplierName
                               OFFSET @Offset ROWS
                               FETCH NEXT @PageSize ROWS ONLY";

            var data = await connection.QueryAsync<Supplier>(
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

        public async Task<Supplier?> GetAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"SELECT *
                           FROM Suppliers
                           WHERE SupplierID = @id";

            return await connection.QueryFirstOrDefaultAsync<Supplier>(sql, new { id });
        }

        public async Task<int> AddAsync(Supplier data)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"INSERT INTO Suppliers
                           (SupplierName, ContactName, Province, Address, Phone, Email)
                           VALUES
                           (@SupplierName, @ContactName, @Province, @Address, @Phone, @Email);
                           SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await connection.ExecuteScalarAsync<int>(sql, data);
        }

        public async Task<bool> UpdateAsync(Supplier data)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"UPDATE Suppliers
                           SET SupplierName = @SupplierName,
                               ContactName = @ContactName,
                               Province = @Province,
                               Address = @Address,
                               Phone = @Phone,
                               Email = @Email
                           WHERE SupplierID = @SupplierID";

            int rows = await connection.ExecuteAsync(sql, data);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"DELETE FROM Suppliers
                           WHERE SupplierID = @id";

            int rows = await connection.ExecuteAsync(sql, new { id });
            return rows > 0;
        }

        public async Task<bool> IsUsedAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"SELECT COUNT(*)
                           FROM Products
                           WHERE SupplierID = @id";

            int count = await connection.ExecuteScalarAsync<int>(sql, new { id });
            return count > 0;
        }
    }
}