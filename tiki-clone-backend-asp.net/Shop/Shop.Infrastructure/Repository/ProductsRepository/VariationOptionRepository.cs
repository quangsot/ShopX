using Dapper;
using Shop.Application.UnitOfWork;
using Shop.Domain.Entity;
using Shop.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repository
{
    public class VariationOptionRepository : WriteRepository<Variationoption>, IVariationOptionRepository
    {
        public VariationOptionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<List<string>> GetVariationOptionByVariationId(int page, int size, Guid Id)
        {
            if(size <= 0 || page <= 0)
            {
                return new List<string>();
            }
            int skip = (page - 1) * size;
            string sql = "SELECT DISTINCT `Value` FROM variationoption WHERE VariationId = @id LIMIT @skip,@size;";

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("id", Id);
            dynamicParameters.Add("skip", skip);
            dynamicParameters.Add("size", size);

            var result = await _unitOfWork.Connection.QueryAsync<string>(sql, dynamicParameters);

            return result.ToList();
        }
    }
}
