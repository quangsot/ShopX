using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interface;
using Shop.Application.Interface.ProductsService;
using Shop.Controllers;
using Shop.Domain.Interface.Repository;
using Shop.Domain.Model.DTO;

namespace Shop.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : WriteController<BrandDTO, BrandCreateDTO, BrandUpdateDTO>
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService) : base(brandService)
        {
            _brandService = brandService;
        }

    }
}
