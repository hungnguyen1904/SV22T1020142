using Microsoft.AspNetCore.Mvc;
using SV22T1020142.BusinessLayers;
using SV22T1020142.Models.Common;
using SV22T1020142.Models.Partner;
using System.Buffers;

namespace SV22T1020142.Admin.Controllers
{
    /// <summary>
    /// Controller xử lý các chức năng quản lý khách hàng trong hệ thống.
    /// Bao gồm: hiển thị danh sách, tìm kiếm, thêm mới, cập nhật,
    /// xóa và thay đổi mật khẩu của khách hàng.
    /// </summary>
    public class CustomerController : Controller
    {
        /// Ten bien dung de luu dieu kien tim kiem khach hang trong session
        /// </summary>
        private const string CUSTOMER_SEARCH = "CustomerSearchInput";
        /// <summary>
        /// Hiển thị danh sách khách hàng có phân trang và tìm kiếm.
        /// </summary>
        /// <param name="page">Trang dữ liệu cần hiển thị</param>
        /// <param name="searchValue">Giá trị tìm kiếm theo tên khách hàng</param>
        /// <returns>Trang danh sách khách hàng</returns>
        public IActionResult Index()
        {
            var input = ApplicationContext.GetSessionData<PaginationSearchInput>(CUSTOMER_SEARCH);
            if (input == null)
                input = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = ApplicationContext.PageSize,
                    SearchValue = ""
                };
            return View(input);
        }
        /// <summary>
        /// Tim kiem va tra ve ket qua
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public async Task<IActionResult> Search(PaginationSearchInput input)
        {
            var result = await PartnerDataService.ListCustomersAsync(input);
            ApplicationContext.SetSessionData(CUSTOMER_SEARCH, input);
            return View(result);
        }


        /// <summary>
        /// Hiển thị form bổ sung khách hàng mới.
        /// </summary>
        /// <returns>Trang nhập thông tin khách hàng</returns>
        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung khách hàng";
            var model = new Customer()
            {
                CustomerID = 0
            };
            return View("Edit", model);
        }

        /// <summary>
        /// Hiển thị form chỉnh sửa thông tin khách hàng.
        /// </summary>
        /// <param name="id">Mã khách hàng cần chỉnh sửa</param>
        /// <returns>Trang chỉnh sửa thông tin khách hàng</returns>
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Title = "Cập nhật khách hàng";
            var data = await PartnerDataService.GetCustomerAsync(id);
            if (data == null)
                return RedirectToAction("Index");

            return View(data);
        }
        /// <summary>
        /// Lưu dữ liệu vào csdl
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        
        public async Task<IActionResult> SaveData(Customer data)
        {
            try
            {
                ViewBag.Title = data.CustomerID == 0 ? "Bổ sung khách hàng" : "Cập nhật khách hàng";

            //kiem tra du lieu dau vao 
            //su dung ModelState de luu thong bao loi va hien thi trang thong bao loi
            //return View("Edit", data);
            if (string.IsNullOrEmpty(data.CustomerName))
            {
                ModelState.AddModelError(nameof(data.CustomerName), "Vui lòng cho biết tên khách hàng");
            }
            if (string.IsNullOrEmpty(data.ContactName))
            {
                ModelState.AddModelError(nameof(data.ContactName), "Tên giao dịch không được để trống");
            }
            if (string.IsNullOrEmpty(data.Phone))
            {
                ModelState.AddModelError("Phone", "Số điện thoại không được để trống");
            }
            if (string.IsNullOrEmpty(data.Email))
            {
                ModelState.AddModelError("Email", "Nhập email của khách hàng");
            }
            else if (!await PartnerDataService.ValidatelCustomerEmailAsync(data.Email, data.CustomerID))
            {
                ModelState.AddModelError("Email", "Email đã tồn tại");
            }
            if (string.IsNullOrEmpty(data.Address))
            {
                ModelState.AddModelError("Address", "Địa chỉ không được để trống");
            }
            if (string.IsNullOrEmpty(data.Province))
            {
                ModelState.AddModelError(nameof(data.Province), "Vui lòng chọn tỉnh thành");
            }

            if (!ModelState.IsValid)
            {
                return View("Edit", data);
            }

            //TODO: xử lý lưu dữ liệu vào csdl
            //Lưu vào csdl
            if (data.CustomerID == 0)
            {
                await PartnerDataService.AddCustomerAsync(data);
            }
            else
            {
                await PartnerDataService.UpdateCustomerAsync(data);
            }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "hệ thống đang lỗi vui lòng thử lại sau");
                return View("Edit", data);
            }
        }

        /// <summary>
        /// Lưu thông tin khách hàng khi thêm mới hoặc cập nhật.
        /// </summary>
        /// <param name="model">Thông tin khách hàng</param>
        /// <returns>Chuyển về trang danh sách sau khi lưu</returns>
        [HttpPost]
        public async Task<IActionResult> Save(Customer model)
        {
            ViewBag.Provinces =
                await DictionaryDataService.ListProvincesAsync();

            if (!ModelState.IsValid)
                return View("Edit", model);

            if (model.CustomerID == 0)
            {
                await PartnerDataService.AddCustomerAsync(model);
            }
            else
            {
                await PartnerDataService.UpdateCustomerAsync(model);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Hiển thị trang xác nhận xóa khách hàng.
        /// </summary>
        /// <param name="id">Mã khách hàng cần xóa</param>
        /// <returns>Trang xác nhận xóa</returns>
        public async Task<IActionResult> Delete(int id)
        {
            if(Request.Method == "POST")
            {   
                await PartnerDataService.DeleteCustomerAsync(id);
                return RedirectToAction("Index");
            }
            var data = await PartnerDataService.GetCustomerAsync(id);

            if (data == null)
                return RedirectToAction("Index");

            ViewBag.CanDelete = !(await PartnerDataService.IsUsedCustomerAsync(id));

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Customer model)
        {
            await PartnerDataService.DeleteCustomerAsync(model.CustomerID);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Hiển thị trang thay đổi mật khẩu của khách hàng.
        /// </summary>
        /// <param name="id">Mã khách hàng</param>
        /// <returns>Trang đổi mật khẩu</returns>
        public async Task<IActionResult> ChangePassword(int id)
        {
            var data = await PartnerDataService.GetCustomerAsync(id);

            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Đổi mật khẩu khách hàng";

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(int customerID, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "Mật khẩu xác nhận không đúng");

                var data = await PartnerDataService.GetCustomerAsync(customerID);
                return View(data);
            }

            // TODO: xử lý đổi mật khẩu (nếu có hàm trong BusinessLayer)

            return RedirectToAction("Index");
        }
    }
}

