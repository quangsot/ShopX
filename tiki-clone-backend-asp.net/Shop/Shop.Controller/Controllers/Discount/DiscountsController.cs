using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interface;
using Shop.Application.Interface.DiscountService;
using Shop.Controllers;
using Shop.Domain.Model.DTO.DiscountDTO;

namespace Shop.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : WriteController<DiscountDTO, DiscountCreateDTO, DiscountUpdateDTO>
    {
        private readonly IDiscountService _discountService;
        public DiscountsController(IDiscountService discountService) : base(discountService)
        {
            _discountService = discountService;
        }


    }
}
