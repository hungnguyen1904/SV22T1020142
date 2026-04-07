using System.ComponentModel.DataAnnotations;

namespace SV22T1020142.Shop.Models
{
    public class CheckoutInput
    {
        [Required(ErrorMessage = "Vui lòng nhập mã khách hàng")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tỉnh/thành giao hàng")]
        public string DeliveryProvince { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng")]
        public string DeliveryAddress { get; set; } = "";
    }
}