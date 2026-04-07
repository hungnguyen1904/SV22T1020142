using System.ComponentModel.DataAnnotations;
using SV22T1020142.Shop.AppCodes;

namespace SV22T1020142.Shop.Models
{
    public class CheckoutViewModel
    {
        [Display(Name = "Họ và tên người nhận")]
        [Required(ErrorMessage = "Vui lòng nhập tên người nhận")]
        public string CustomerName { get; set; } = "";

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string Phone { get; set; } = "";

        [Display(Name = "Địa chỉ giao hàng")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng")]
        public string DeliveryAddress { get; set; } = "";

        [Display(Name = "Tỉnh/Thành")]
        [Required(ErrorMessage = "Vui lòng nhập tỉnh/thành")]
        public string DeliveryProvince { get; set; } = "";

        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public decimal TotalAmount => Items.Sum(x => x.TotalPrice);
    }
}
