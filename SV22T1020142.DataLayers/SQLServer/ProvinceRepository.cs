using Dapper;
using Microsoft.Data.SqlClient;
using SV22T1020142.DataLayers.Interfaces;
using SV22T1020142.Models.DataDictionary;

namespace SV22T1020142.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt các chức năng xử lý dữ liệu cho bảng Provinces
    /// </summary>
    public class ProvinceRepository : IDataDictionaryRepository<Province>
    {
        private readonly string _connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProvinceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Lấy danh sách tỉnh/thành
        /// </summary>
        public async Task<List<Province>> ListAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            string sql = @"SELECT ProvinceName
                           FROM Provinces
                           ORDER BY ProvinceName";

            var data = await connection.QueryAsync<Province>(sql);
            return data.ToList();
        }
    }
}