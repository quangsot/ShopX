using Shop.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interface.Repository
{
    public interface IVariationOptionRepository : IWriteRepository<Variationoption>
    {
        /// <summary>
        /// lấy các giá trị của 1 biến thể
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<List<string>> GetVariationOptionByVariationId(int page, int size, Guid Id);
    }
}
