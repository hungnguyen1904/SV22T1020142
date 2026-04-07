using SV22T1020142.DataLayers.Interfaces;
using SV22T1020142.DataLayers.SQLServer;
using SV22T1020142.Models.DataDictionary;

namespace SV22T1020142.BusinessLayers
{
    /// <summary>
    /// Lớp cung cấp các chức năng xử lý dữ liệu cho từ điển dữ liệu
    /// </summary>
    public class DictionaryDataService
    {
        private static readonly IDataDictionaryRepository<Province> provinceDB;

        /// <summary>
        /// Constructor
        /// </summary>
        static DictionaryDataService()
        {
            provinceDB = new ProvinceRepository(Configuration.ConnectionString);
        }

        /// <summary>
        /// Lấy danh sách tỉnh/thành
        /// </summary>
        public static async Task<List<Province>> ListProvincesAsync()
        {
            return await provinceDB.ListAsync();
        }
    }
}