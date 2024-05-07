using Dapper;
using Shop.Application.UnitOfWork;
using Shop.Domain.Entity;
using Shop.Domain.Interface.Repository;
using Shop.Domain.Model.Output;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repository
{
    public class VariationRepository : WriteRepository<Variation>, IVariationRepositry
    {
        public VariationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<FilterPaging<Variation>> FillterPagingAsync(int pageNumber, int pageSize, string search)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return new FilterPaging<Variation>();
            }
            else
            {
                int totalRecord;
                int totalPage;
                int currentPage;
                int currentRecord;
                string sql = $"SELECT #output FROM {TableName} WHERE #search #paging";
                DynamicParameters parameters = new();

                if (string.IsNullOrEmpty(search))
                {
                    sql = sql.Replace("#search", "1=1");
                }
                else
                {
                    sql = sql.Replace("#search", "`Name` LIKE @search");
                    parameters.Add("@search", $"%{search}%", System.Data.DbType.String);
                }

                // count total record
                string sqlCountRecord = sql.Replace("#output", "COUNT(*)");
                sqlCountRecord = sqlCountRecord.Replace("#paging", " ");

                totalRecord = await _dbConnection.QuerySingleAsync<int>(sqlCountRecord, parameters);

                ////
                if (totalRecord == 0)
                {
                    return new FilterPaging<Variation>();
                }
                else
                {
                    totalPage = (int)Math.Ceiling(totalRecord * 1.0 / pageSize);
                    currentPage = Math.Min(totalPage, pageNumber);
                    currentRecord = currentPage < totalPage ? pageSize : totalRecord - ((currentPage - 1) * pageSize);
                    int skipRecord = (currentPage - 1) * pageSize;

                    string sqlFilterPaging = sql.Replace("#output", "*");
                    sqlFilterPaging = sqlFilterPaging.Replace("#paging", "LIMIT @skipRecord,@pageSize");

                    parameters.Add("skipRecord", skipRecord);
                    parameters.Add("pageSize", pageSize);

                    var listRecord = await _dbConnection.QueryAsync<Variation>(sqlFilterPaging, parameters);

                    return new FilterPaging<Variation>()
                    {
                        TotalRecord = totalRecord,
                        TotalPage = totalPage,
                        Size = currentRecord,
                        Page = currentPage,
                        Items = listRecord.AsList(),
                    };
                }
            }
        }

        public async Task<Variation> GetByNameAsync(string name, DbTransaction? dbTransaction = null)
        {
            string sql = $"Select v.Id, v.Name From {TableName} v Where Name = @name";
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("name", name);
            var result = await _dbConnection.QuerySingleOrDefaultAsync<Variation>(sql, dynamicParameters, dbTransaction);
            return result;
        }
        public async Task<List<Variation>> GetByNamesAsync(List<string> names, DbTransaction? dbTransaction = null)
        {
            string sql = $"Select v.Id, v.Name, v.Discription From {TableName} v Where Name IN @names";
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("names", names);
            var result = await _dbConnection.QueryAsync<Variation>(sql, dynamicParameters, dbTransaction);
            return result.ToList();
        }

        public async Task<bool> IsHasVariationNameAsync(string name)
        {
            string sql = $"Select Id From {TableName} v Where v.Name = @name";
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("name", name);
            var result = await _dbConnection.QueryFirstOrDefaultAsync(sql, dynamicParameters);
            return result != null;
        }
    }
}
