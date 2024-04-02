﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Model.DTO
{
    public class RoleDTO
    {
        public Guid Id { get; set; }

        public int? Code { get; set; }

        public string? Name { get; set; }

        public string? Discription { get; set; }

    }
}
