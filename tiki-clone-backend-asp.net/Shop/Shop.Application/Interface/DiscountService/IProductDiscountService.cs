using Shop.Application.Services.Base;
using Shop.Domain.Model.DTO.DiscountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interface.DiscountService
{
    public interface IProductDiscountService : IWriteService<ProductDiscountDTO, ProductDiscountCreateDTO, ProductDiscountDTO>
    {
    }
}
