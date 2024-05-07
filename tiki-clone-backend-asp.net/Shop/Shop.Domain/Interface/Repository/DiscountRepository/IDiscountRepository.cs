﻿using Shop.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interface.Repository.DiscountRepository
{
    public interface IDiscountRepository : IWriteRepository<Discount>
    {
    }
}
