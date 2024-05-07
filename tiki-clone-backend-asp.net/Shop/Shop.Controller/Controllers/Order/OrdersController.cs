using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interface;
using Shop.Application.Interface.OrderService;
using Shop.Controllers;
using Shop.Domain.Interface.UseCases;
using Shop.Domain.Model.DTO;
using Shop.Domain.Model.DTO.DiscountDTO;
using Shop.Domain.Model.Response;
using Shop.Infrastructure.Repository;

namespace Shop.Controller.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderUseCase _orderUseCase;
        //private readonly IOrderService _orderService;
        public OrdersController(IOrderUseCase orderUseCase)
        {
            _orderUseCase = orderUseCase;
            //_orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] OrderResponse entityCreateDTO)
        {
            var ressult = await _orderUseCase.CreateOrderAsync(entityCreateDTO);
            return Ok(ressult);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateStatusOrder(Guid id, OrderStatus orderStatus)
        {
            var result = await _orderUseCase.UpdateStatusOrderAsync(id, orderStatus);
            return Ok(result);
        }
    }
}
