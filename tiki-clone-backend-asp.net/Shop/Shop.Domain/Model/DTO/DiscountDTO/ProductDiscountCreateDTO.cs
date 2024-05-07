using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Model.DTO.DiscountDTO
{
    public class ProductDiscountCreateDTO
    {
        public Guid? DiscountId { get; set; }

        public Guid? ProductsId { get; set; }

        public sbyte? Status { get; set; }
    }
}
