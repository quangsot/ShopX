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
    public class DiscountService : WriteService<Discount, DiscountDTO, DiscountCreateDTO, DiscountUpdateDTO>, IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountService(IDiscountRepository discountRepository, IMapper mapper) : base(discountRepository, mapper)
        {
            _discountRepository = discountRepository;
        }

        protected override Task EditData(Discount entity)
        {
            entity.Id = Guid.NewGuid();
            return Task.CompletedTask;
        }
    }
}
