using AutoMapper;
using Shop.Application.Interface.DiscountService;
using Shop.Application.Services.Base;
using Shop.Domain.Entity;
using Shop.Domain.Interface.Repository;
using Shop.Domain.Interface.Repository.DiscountRepository;
using Shop.Domain.Model.DTO.DiscountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services.DiscountService
{
    public class ProductDiscountService : WriteService<Productdiscount, ProductDiscountDTO, ProductDiscountCreateDTO, ProductDiscountDTO>, IProductDiscountService
    {
        private readonly IProductDiscountRepository _productDiscountRepository;
        public ProductDiscountService(IProductDiscountRepository productDiscountRepository, IMapper mapper) : base(productDiscountRepository, mapper)
        {
            _productDiscountRepository = productDiscountRepository;
        }
    }
}
