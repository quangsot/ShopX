using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interface;
using Shop.Application.Interface.DiscountService;
using Shop.Controllers;
using Shop.Domain.Model.DTO.DiscountDTO;

namespace Shop.Controller.Controllers.Discount
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDiscountsController : WriteController<ProductDiscountDTO, ProductDiscountCreateDTO, ProductDiscountDTO>
    {
        private readonly IProductDiscountService _productDiscountService;
        public ProductDiscountsController(IProductDiscountService productDiscountService) : base(productDiscountService)
        {
            _productDiscountService = productDiscountService;
        }
    }
}
