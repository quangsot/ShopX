using Shop.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interface.Repository
{
    public interface IVariationOptionGroupRepository : IWriteRepository<Variationoptiongroup>
    {
        /// <summary>
        /// lấy ra nhóm biến thể, nếu tồn tại trả về id, ngược lại trả về null
        /// </summary>
        /// <param name="variations"></param>
        /// <returns></returns>
        Task<Guid?> GetVariationGroupIdByVariation(Dictionary<string, string> variations);
    }
}
