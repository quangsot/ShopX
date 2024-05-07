using Dapper;
using Shop.Application.UnitOfWork;
using Shop.Domain.Entity;
using Shop.Domain.Interface.Repository;
using Shop.Domain.Model.DTO;
using Shop.Domain.Model.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repository
{
    public class ProductRepository : WriteRepository<Product>, IProductRepository
    {
        private readonly IVariationRepositry _variationRepositry;
        public ProductRepository(IUnitOfWork unitOfWork, IVariationRepositry variationRepositry) : base(unitOfWork)
        {
            _variationRepositry = variationRepositry;
        }

        public async Task<FilterPaging<ProductResponse>> FilterPagingProductAsync(int page, int size, string orderBy, Dictionary<string, string> conditions)
        {
            string sql = "SELECT #output FROM product p " +
                "JOIN category c ON p.CategoryId = c.Id " +
                "JOIN brand b ON p.BrandId = b.Id " +
                "JOIN supplier s ON p.SupplierId = s.Id " +
                "JOIN productconfiguration p1 ON p.Id = p1.ProductId " +
                "JOIN variationoptiongroup v ON p1.VariationOptionGroupId = v.Id " +
                "JOIN variationoption v1 ON v.Id = v1.VariationOptionGroupId " +
                "JOIN variation v2 ON v1.VariationId = v2.Id " +
                "WHERE 1 = 1 " +
                "#condition #paging";

            string condition = string.Empty;
            DynamicParameters parameters = new();

            if (conditions.ContainsKey("date"))
            {
                string date = conditions["date"];
                var dateRange = date.Split(',');
                if (dateRange[0] != null)
                {
                    condition += $"AND p.CreatedAt >= @startDate ";
                    parameters.Add("startDate", dateRange[0]);
                }
                if (dateRange[1] != null)
                {
                    condition += $"AND p.CreatedAt <= @endDate ";
                    parameters.Add("endDate", dateRange[1]);
                }
                conditions.Remove("date");
            }

            if (conditions.ContainsKey("category"))
            {
                string category = conditions["category"];
                condition += $"AND c.Name = @category ";
                parameters.Add("category", category);
                conditions.Remove("category");
            }

            if (conditions.ContainsKey("supplier"))
            {
                string supplier = conditions["supplier"];
                condition += $"AND s.Name = @supplier ";
                parameters.Add("supplier", supplier);
                conditions.Remove("supplier");
            }

            if (conditions.ContainsKey("brand"))
            {
                string brand = conditions["brand"];
                condition += $"AND b.Name = @brand ";
                parameters.Add("brand", brand);
                conditions.Remove("brand");
            }

            if (conditions.ContainsKey("price"))
            {
                string price = conditions["price"];
                var priceRange = price.Split(',');
                if (priceRange[0] != null)
                {
                    condition += $"AND p1.Price >= @priceMin ";
                    parameters.Add("priceMin", priceRange[0]);
                }
                if (priceRange[1] != null)
                {
                    condition += $"AND p1.Price <= @priceMax ";
                    parameters.Add("priceMax", priceRange[1]);
                }
                conditions.Remove("price");
            }

            if (conditions != null)
            {
                foreach (var c in conditions)
                {
                    // check variation
                    if (await _variationRepositry.IsHasVariationNameAsync(c.Key))
                    {
                        condition += $"AND v1.Value = @variationOption AND v2.Name = @variation";
                        parameters.Add("variationOption", c.Value);
                        parameters.Add("variation", c.Key);
                    }
                }
            }

            string sqlCountRecord = sql.Replace("#condition", condition);

            sqlCountRecord = sqlCountRecord.Replace("#output", "COUNT(p.Id)");
            sqlCountRecord = sqlCountRecord.Replace("#paging", string.Empty);

            var totalRecord = await _dbConnection.QuerySingleAsync<int>(sqlCountRecord, parameters);
            if (totalRecord == 0)
            {
                return new FilterPaging<ProductResponse>();
            }
            else
            {
                int totalPage = (int)Math.Ceiling(totalRecord * 1.0 / size);
                int currentPage = Math.Min(totalPage, page);
                int currentRecord = currentPage < totalPage ? size : totalRecord - ((currentPage - 1) * size);
                int skipRecord = (currentPage - 1) * size;

                string sort = string.Empty;
                if (!string.IsNullOrEmpty(orderBy))
                {
                    sort = orderBy.Replace(",", " ");
                    condition += $" ORDER BY @sort";
                    parameters.Add("sort", sort);
                }

                string filterSql = sql.Replace("#output", "p.Id, p.Name ,b.Name AS BrandName, p.Avatar, p.TotalSold, p.Hot, p.AverageStar, p1.Price, p1.Inventory");
                filterSql = filterSql.Replace("#paging", "LIMIT @skipRecord,@pageSize");

                parameters.Add("skipRecord", skipRecord);
                parameters.Add("pageSize", size);

                filterSql = filterSql.Replace("#condition", condition);

                var result = await _dbConnection.QueryAsync<ProductResponse>(filterSql, parameters);

                return new FilterPaging<ProductResponse>
                {
                    TotalPage = totalPage,
                    TotalRecord = totalRecord,
                    Page = page,
                    Size = size,
                    Sort = sort,
                    Items = result.ToList()
                };
            }
        }
    }
}
