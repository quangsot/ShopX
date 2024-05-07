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
    public class VariationOptionGroupRepository : WriteRepository<Variationoptiongroup>, IVariationOptionGroupRepository
    {
        public VariationOptionGroupRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Guid?> GetVariationGroupIdByVariation(Dictionary<string, string> variations)
        {
            string sql = string.Empty;
            DynamicParameters parameters = new();
            if (variations.Count == 1)
            {
                sql =
                    "SELECT Id FROM " +
                    "( SELECT vog.Id Id, v.Name AS `Name`, vo.Value AS `Value` FROM variationoptiongroup vog " +
                    "JOIN variationoption vo ON vog.Id = vo.VariationOptionGroupId " +
                    "JOIN variation v ON vo.VariationId = v.Id " +
                    "GROUP BY vog.Id " +
                    "HAVING COUNT(v.Id) = 1) v " +
                    "WHERE (`Name` = @key AND `Value` = @value)";

                parameters.Add("key", variations.Keys.First());
                parameters.Add("value", variations.Values.First());
            }
            else if (variations.Count == 2)
            {
                sql =
                    "SELECT vog.Id FROM variationoptiongroup vog " +
                    "JOIN variationoption vo ON vog.Id = vo.VariationOptionGroupId " +
                    "JOIN variation v ON vo.VariationId = v.Id " +
                    "WHERE (v.Name = @key1 AND vo.Value = @value1)" +
                    "UNION" +
                    "SELECT vog.Id FROM variationoptiongroup vog " +
                    "JOIN variationoption vo ON vog.Id = vo.VariationOptionGroupId " +
                    "JOIN variation v ON vo.VariationId = v.Id " +
                    "WHERE (v.Name = @key2 AND vo.Value = @value2)";
                parameters.Add("key1", variations.Keys.First());
                parameters.Add("value1", variations.Values.First());
                parameters.Add("key2", variations.Keys.Last());
                parameters.Add("value2", variations.Values.Last());
            }
            else return null;
            

            Guid? result;
            result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Guid>(sql, parameters);

            return result;
        }
    }
}
