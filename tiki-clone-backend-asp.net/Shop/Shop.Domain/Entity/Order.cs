using System;
using System.Collections.Generic;

namespace Shop.Domain.Entity;

public partial class Order
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public Guid? UserId { get; set; }

    public string? ShippingAddress { get; set; }

    public double? TotalPrice { get; set; }

    public int OrderStatus { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? PaymentTypeId { get; set; }

    public virtual Orderstatus OrderStatusNavigation { get; set; } = null!;

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual Paymenttype? PaymentType { get; set; }

    public virtual User? User { get; set; }
}
