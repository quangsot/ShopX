﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Model.DTO.DiscountDTO
{
    public class DiscountDTO
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

    }
}
