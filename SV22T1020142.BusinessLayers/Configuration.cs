using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SV22T1020142.BusinessLayers
{
    public class Configuration
    {
        private static string _connectionString = "";
        /// <summary>
        ///  Khoi tao cau hinh cho 
        /// </summary>
        /// <param name="connectionString"></param>

        public static void Initialize (string connectionString)
        {
            _connectionString = connectionString;
        }
        public static string ConnectionString => _connectionString;
    }
}
