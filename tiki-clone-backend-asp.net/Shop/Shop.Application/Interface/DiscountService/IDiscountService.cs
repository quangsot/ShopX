using Shop.Domain.Model.DTO.DiscountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interface.DiscountService
{
    public interface IDiscountService: IWriteService<DiscountDTO, DiscountCreateDTO, DiscountUpdateDTO>
    {
    }
}
