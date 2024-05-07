using AutoMapper;
using Shop.Application.Interface.OrderService;
using Shop.Application.Services.Base;
using Shop.Domain.Entity;
using Shop.Domain.Interface.Repository;
using Shop.Domain.Interface.Repository.OrdersRepository;
using Shop.Domain.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services.OrderService
{
    public class OrderService : WriteService<Order, OrderDTO, OrderDTO, OrderDTO>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository, IMapper mapper) : base(orderRepository, mapper)
        {
            _orderRepository = orderRepository;
        }
    }
}
