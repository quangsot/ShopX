using Shop.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Model.Response
{
    public class OrderResponse
    {
        [Required(ErrorMessage = "Mã đơn hàng không được để trống.")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Id người nhận hàng không được để trống.")]
        public Guid UserId { get; set; } = Guid.Empty;

        [Required(ErrorMessage = "Địa chỉ giao hàng không được để trống.")]
        public string ShippingAddress { get; set; }

        public double TotalPrice { get; set; }

        public int OrderStatus { get; set; }

        public DateTime? OrderDate { get; set; } = DateTime.Now;

        public int PaymentTypeId { get; set; }

        public List<OrderItemResponse> OrderItems { get; set; } = new();

    }

    public class OrderItemResponse
    {
        public Guid ProductDetailId { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public double TotalPrice { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public string ProductAvatar { get; set; } = string.Empty;
    }
}
