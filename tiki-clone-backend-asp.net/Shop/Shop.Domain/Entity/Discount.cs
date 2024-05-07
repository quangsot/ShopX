using System;
using System.Collections.Generic;

namespace Shop.Domain.Entity;

public partial class Discount
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Type { get; set; }

    public double Value { get; set; }

    public int Quantity { get; set; }

    public sbyte Status { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<Productdiscount> Productdiscounts { get; set; } = new List<Productdiscount>();
}
