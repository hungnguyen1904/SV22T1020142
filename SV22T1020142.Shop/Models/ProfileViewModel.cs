using System.ComponentModel.DataAnnotations;

namespace SV22T1020142.Shop.Models
{
    public class ProfileViewModel
    {
        public int CustomerID { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string CustomerName { get; set; } = "";

        [Display(Name = "Tên liên hệ")]
        [Required(ErrorMessage = "Vui lòng nhập tên liên hệ")]
        public string ContactName { get; set; } = "";

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = "";

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string Phone { get; set; } = "";

        [Display(Name = "Tỉnh/Thành")]
        [Required(ErrorMessage = "Vui lòng nhập tỉnh/thành")]
        public string Province { get; set; } = "";

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string Address { get; set; } = "";
    }
}
