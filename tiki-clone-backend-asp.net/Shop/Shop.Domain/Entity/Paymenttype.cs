using System;
using System.Collections.Generic;

namespace Shop.Domain.Entity;

public partial class Paymenttype
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
