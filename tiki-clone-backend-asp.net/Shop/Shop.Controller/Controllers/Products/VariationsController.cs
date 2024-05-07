using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interface;
using Shop.Application.Interface.ProductsService;
using Shop.Controllers;
using Shop.Domain.Model.DTO;
using Shop.Domain.Model.Response;

namespace Shop.Controller.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariationsController : WriteController<VariationDTO, VariationDTO, VariationDTO>
    {
        private readonly IVariationService _variationService;
        private readonly IVariationOptionService _variationOptionService;
        public VariationsController(IVariationOptionService variationOptionService, IVariationService variationService) : base(variationService)
        {
            _variationService = variationService;
            _variationOptionService = variationOptionService;
        }

        [HttpGet("variation-option")]
        public async Task<IActionResult> GetVariationOptionByVariation(Guid variationId, int page = 1, int size = 10)
        {
            var result = await _variationOptionService.GetVariationOptionByVariationId(variationId, page, size);
            return Ok(new PageResponse<string>()
            {
                Data = new Domain.Model.Output.FilterPaging<string>
                {
                    Items = result
                }
            });
        }
    }
}
