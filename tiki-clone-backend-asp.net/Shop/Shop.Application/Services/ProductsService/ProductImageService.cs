﻿using AutoMapper;
using Shop.Application.Interface;
using Shop.Application.Services.Base;
using Shop.Domain.Entity;
using Shop.Domain.Interface.Repository;
using Shop.Domain.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services.ProductsService
{
    public class ProductImageService : WriteService<Productimage, ProductImageDTO, ProductImageDTO, ProductImageDTO>, IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        public ProductImageService(IProductImageRepository productImageRepository, IMapper mapper) : base(productImageRepository, mapper)
        {
            _productImageRepository = productImageRepository;
        }

        public async Task<List<Productimage>> GetImagesByProductDetailIdAsync(Guid productDetailId)
        {
            var result = await _productImageRepository.GetImagesByProductDetailAsync(productDetailId);
            return result;
        }

        protected override Task EditData(Productimage entity)
        {
            return Task.CompletedTask;
        }

        protected override Task ValidateLogicBusiness(Productimage entity)
        {
            return Task.CompletedTask;
        }
    }
}
