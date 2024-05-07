using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Model.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }

        public string? Code { get; set; }

        public Guid? UserId { get; set; }

        public string? ShippingAddress { get; set; }

        public double? TotalPrice { get; set; }

        public int OrderStatus { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? PaymentTypeId { get; set; }
    }
}
