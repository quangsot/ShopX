﻿using Dapper;
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
    public class ProductImageRepository : WriteRepository<Productimage>, IProductImageRepository
    {
        public ProductImageRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<Productimage>> GetImagesByProductDetailAsync(Guid productDetailId)
        {
            string sql = "SELECT * FROM productimage " +
                    "WHERE productimage.ProductDetailId = @productDetailId;";
            DynamicParameters parameters = new();
            parameters.Add("@productDetailId", productDetailId);
            var result = await _dbConnection.QueryAsync<Productimage>(sql, parameters);
            return result.ToList();
        }
    }
}
