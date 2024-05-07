using Shop.Domain.Entity;
using Shop.Domain.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interface.ProductsService
{
    public interface IVariationOptionService : IWriteService<VariationOptionDTO, VariationOptionDTO, VariationOptionDTO>
    {
        /// <summary>
        /// phân trang giá trị biến thể
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<List<string>> GetVariationOptionByVariationId(Guid Id, int page = 1, int size = 10);
    }
}
