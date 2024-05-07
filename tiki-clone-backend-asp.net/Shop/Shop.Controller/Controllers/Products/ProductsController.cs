using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interface;
using Shop.Application.Interface.ProductsService;
using Shop.Controller;
using Shop.Domain.CustomAttibute;
using Shop.Domain.Entity;
using Shop.Domain.Interface.UseCases;
using Shop.Domain.Model.DTO;
using Shop.Domain.Model.Request;
using Shop.Domain.Model.Response.Base;

namespace Shop.Controllers
{

    [Route("api/")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductUseCase _productUseCase;
        private readonly IProductService _productService;
        public ProductsController(IProductUseCase productUseCase, IProductService productService)
        {
            _productUseCase = productUseCase;
            _productService = productService;
        }

        [XAuthorize(Roles.Admin)]
        [HttpPost("product")]
        public async Task<IActionResult> AddNewProduct([FromForm] ProductForm productForm)
        {
            var result = await _productUseCase.AddNewProduct(productForm);
            return Ok(new BaseResponse<Guid>() { Data = result });
        }

        [XAuthorize(Roles.Admin)]
        [HttpDelete("product")]
        public async Task<IActionResult> DeleteProduct([FromQuery] Guid id)
        {
            var result = await _productService.DeleteAsync(id);
            return Ok(new BaseResponse<ProductDTO>() { Data = result});
        }

        [XAllowAnonymous]
        [HttpGet("{filter}/")]
        public async Task<IActionResult> FilterPagingProductAsync([FromQuery] Dictionary<string, string> conditions)
        {
            var result = await _productUseCase.PagingFilterProductByCategoryAsync(conditions);
            return Ok(result);
        }

        [XAllowAnonymous]
        [HttpGet("product/detail")]
        public async Task<IActionResult> GetDetailProductAsync([FromQuery] Guid productId)
        {
            var result = await _productUseCase.GetProductDetailAsync(productId);
            return Ok(result);
        }
        [XAllowAnonymous]
        [HttpPost("product/test")]
        public IActionResult Test([FromForm]UploadFile uploadFile)
        {
            var test = uploadFile;
            if (test.images.Count > 0) return Ok($"{test.Name} - {test.images.Count}");
            else return Ok(0);
        }
    }
}
