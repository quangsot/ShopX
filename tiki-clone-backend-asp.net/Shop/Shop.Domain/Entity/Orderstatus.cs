using System;
using System.Collections.Generic;

namespace Shop.Domain.Entity;

public partial class Orderstatus
{
    public int Id { get; set; }

    public string? StatusName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
