using Dapper;
using Microsoft.Data.SqlClient;
using SV22T1020142.BusinessLayers;
using SV22T1020142.Shop.AppCodes;
using SV22T1020142.Shop.Models;

namespace SV22T1020142.Shop.Services
{
    public static class ShopCustomerDataService
    {
        public static async Task<CustomerSessionData?> AuthorizeAsync(string email, string password)
        {
            using var connection = new SqlConnection(Configuration.ConnectionString);

            const string sql = @"SELECT CustomerID,
                                        CustomerName,
                                        ContactName,
                                        Email,
                                        Phone,
                                        Province,
                                        Address,
                                        Password,
                                        IsLocked
                                 FROM Customers
                                 WHERE LOWER(Email) = LOWER(@email)";

            var customer = await connection.QueryFirstOrDefaultAsync<CustomerAccountRecord>(sql, new { email });
            if (customer == null || customer.IsLocked)
                return null;

            string hashedPassword = PasswordHelper.HashPassword(password);
            if (!string.Equals(customer.Password, hashedPassword, StringComparison.OrdinalIgnoreCase))
                return null;

            return ToSessionData(customer);
        }

        public static async Task<(bool Success, string ErrorMessage, CustomerSessionData? Customer)> RegisterAsync(RegisterViewModel model)
        {
            using var connection = new SqlConnection(Configuration.ConnectionString);

            const string checkEmailSql = @"SELECT COUNT(*)
                                           FROM Customers
                                           WHERE LOWER(Email) = LOWER(@email)";

            int exists = await connection.ExecuteScalarAsync<int>(checkEmailSql, new { email = model.Email });
            if (exists > 0)
                return (false, "Email này đã được sử dụng.", null);

            const string insertSql = @"INSERT INTO Customers
                                       (CustomerName, ContactName, Province, Address, Phone, Email, Password, IsLocked)
                                       VALUES
                                       (@CustomerName, @ContactName, @Province, @Address, @Phone, @Email, @Password, 0);
                                       SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int customerID = await connection.ExecuteScalarAsync<int>(
                insertSql,
                new
                {
                    model.CustomerName,
                    model.ContactName,
                    model.Province,
                    model.Address,
                    model.Phone,
                    model.Email,
                    Password = PasswordHelper.HashPassword(model.Password)
                });

            var customer = await GetSessionDataAsync(customerID);
            return customer == null
                ? (false, "Không thể tạo tài khoản mới.", null)
                : (true, "", customer);
        }

        public static async Task<ProfileViewModel?> GetProfileAsync(int customerID)
        {
            using var connection = new SqlConnection(Configuration.ConnectionString);

            const string sql = @"SELECT CustomerID,
                                        CustomerName,
                                        ContactName,
                                        Email,
                                        Phone,
                                        Province,
                                        Address,
                                        IsLocked
                                 FROM Customers
                                 WHERE CustomerID = @customerID";

            return await connection.QueryFirstOrDefaultAsync<ProfileViewModel>(sql, new { customerID });
        }

        public static async Task<bool> UpdateProfileAsync(ProfileViewModel model)
        {
            using var connection = new SqlConnection(Configuration.ConnectionString);

            const string checkEmailSql = @"SELECT COUNT(*)
                                           FROM Customers
                                           WHERE LOWER(Email) = LOWER(@Email)
                                             AND CustomerID <> @CustomerID";

            int duplicated = await connection.ExecuteScalarAsync<int>(checkEmailSql, model);
            if (duplicated > 0)
                return false;

            const string sql = @"UPDATE Customers
                                 SET CustomerName = @CustomerName,
                                     ContactName = @ContactName,
                                     Email = @Email,
                                     Phone = @Phone,
                                     Province = @Province,
                                     Address = @Address
                                 WHERE CustomerID = @CustomerID";

            int rows = await connection.ExecuteAsync(sql, model);
            return rows > 0;
        }

        public static async Task<(bool Success, string ErrorMessage)> ChangePasswordAsync(
            int customerID,
            string currentPassword,
            string newPassword)
        {
            using var connection = new SqlConnection(Configuration.ConnectionString);

            const string sql = @"SELECT Password
                                 FROM Customers
                                 WHERE CustomerID = @customerID";

            string? currentHash = await connection.ExecuteScalarAsync<string?>(sql, new { customerID });
            if (string.IsNullOrWhiteSpace(currentHash))
                return (false, "Không tìm thấy tài khoản khách hàng.");

            if (!string.Equals(currentHash, PasswordHelper.HashPassword(currentPassword), StringComparison.OrdinalIgnoreCase))
                return (false, "Mật khẩu hiện tại không đúng.");

            const string updateSql = @"UPDATE Customers
                                       SET Password = @Password
                                       WHERE CustomerID = @customerID";

            await connection.ExecuteAsync(
                updateSql,
                new
                {
                    customerID,
                    Password = PasswordHelper.HashPassword(newPassword)
                });

            return (true, "");
        }

        public static async Task<bool> SyncCheckoutProfileAsync(int customerID, CheckoutViewModel model)
        {
            using var connection = new SqlConnection(Configuration.ConnectionString);

            const string sql = @"UPDATE Customers
                                 SET CustomerName = @CustomerName,
                                     Province = @Province,
                                     Address = @Address,
                                     Phone = @Phone
                                 WHERE CustomerID = @customerID";

            int rows = await connection.ExecuteAsync(
                sql,
                new
                {
                    customerID,
                    model.CustomerName,
                    Province = model.DeliveryProvince,
                    Address = model.DeliveryAddress,
                    model.Phone
                });

            return rows > 0;
        }

        public static async Task<CustomerSessionData?> GetSessionDataAsync(int customerID)
        {
            using var connection = new SqlConnection(Configuration.ConnectionString);

            const string sql = @"SELECT CustomerID,
                                        CustomerName,
                                        ContactName,
                                        Email,
                                        Phone,
                                        Province,
                                        Address,
                                        IsLocked
                                 FROM Customers
                                 WHERE CustomerID = @customerID";

            var customer = await connection.QueryFirstOrDefaultAsync<CustomerAccountRecord>(sql, new { customerID });
            if (customer == null || customer.IsLocked)
                return null;

            return ToSessionData(customer);
        }

        private static CustomerSessionData ToSessionData(CustomerAccountRecord customer)
        {
            return new CustomerSessionData
            {
                CustomerID = customer.CustomerID,
                CustomerName = customer.CustomerName,
                ContactName = customer.ContactName,
                Email = customer.Email,
                Phone = customer.Phone ?? "",
                Province = customer.Province ?? "",
                Address = customer.Address ?? ""
            };
        }

        private class CustomerAccountRecord
        {
            public int CustomerID { get; set; }
            public string CustomerName { get; set; } = "";
            public string ContactName { get; set; } = "";
            public string Email { get; set; } = "";
            public string? Phone { get; set; }
            public string? Province { get; set; }
            public string? Address { get; set; }
            public string? Password { get; set; }
            public bool IsLocked { get; set; }
        }
    }
}
