using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shop.Application.UnitOfWork;
using Shop.Domain.Entity;
using Shop.Domain.Enum;
using Shop.Domain.Exceptions;
using Shop.Domain.Interface.Repository;
using Shop.Domain.Interface.Repository.OrdersRepository;
using Shop.Domain.Interface.UseCases;
using Shop.Domain.Model.DTO;
using Shop.Domain.Model.Request;
using Shop.Domain.Model.Response;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.UseCases
{
    public class OrderUseCase : IOrderUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public OrderUseCase(IMapper mapper, IUnitOfWork unitOfWork, IUserRepository userRepository, IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IShoppingCartItemRepository shoppingCartItemRepository, IProductDetailRepository productDetailRepository)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _productDetailRepository = productDetailRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddProductToShoppingCart(Guid productDetailId, int quantity, HttpContext httpContext)
        {
            // kiểm trả sản phẩm có tồn tại không
            if (!await _productDetailRepository.IsHasProductDetail(productDetailId))
            {
                // kiểm tra số lượng có lớn hơn 0 không
                if (quantity > 0)
                {
                    var user = (AuthorizeUser)httpContext.Items["User"];
                    var cartItem = new Shoppingcartitem()
                    {
                        Id = new Guid(),
                        UserId = user.Id,
                        ProductsDetailId = productDetailId,
                        Quantity = quantity
                    };
                    _ = await _shoppingCartItemRepository.CreateAsync(cartItem, null);
                }
                else
                {
                    throw new BadRequestException(ErrorCode.Invalid, $"Số lượng sản phẩm phải lớn hơn 0.");
                }

            }
            else throw new BadRequestException(ErrorCode.Invalid, $"Sản phẩm đã tồn tại trong giỏ hàng.");
        }

        public async Task<OrderResponse> CreateOrderAsync(OrderResponse orderResponse)
        {
            if (orderResponse == null)
                throw new BadRequestException(ErrorCode.InvalidInput, "Thông tin Order không tìm thấy.");
            else if (orderResponse.OrderItems == null || orderResponse?.OrderItems.Count <= 0)
                throw new BadRequestException(ErrorCode.InvalidInput, "Thông tin Order item không tìm thấy.");
            else
            {
                // kiểm tra user có tồn tại không
                _ = await _userRepository.GetByIdAsync(orderResponse.UserId)
                    ?? throw new BadRequestException(ErrorCode.InvalidInput, "Người dùng không tồn tại trong hệ thống.");

                var order = new Order()
                {
                    Id = Guid.NewGuid(),
                    Code = orderResponse.Code,
                    UserId = orderResponse.UserId,
                    ShippingAddress = orderResponse.ShippingAddress,
                    TotalPrice = orderResponse.TotalPrice,
                    OrderDate = orderResponse.OrderDate,
                    OrderStatus = orderResponse.OrderStatus,
                    PaymentTypeId = orderResponse.PaymentTypeId,
                };
                //var orderCreated = await _orderRepository.CreateAsync(order, transaction);
                _ = _unitOfWork.DbContext.Add<Order>(order);
                var orderItems = new List<Orderitem>();
                foreach (var item in orderResponse.OrderItems)
                {
                    orderItems.Add(new Orderitem()
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        ProductDetailId = item.ProductDetailId,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice,
                        ProductName = item.ProductName,
                        ProductAvatar = item.ProductAvatar
                    });
                }
                _unitOfWork.DbContext.AddRange(orderItems);
                //var orderItemsCreated = _orderItemRepository.CreateManyAsync(orderItems, transaction);
                _unitOfWork.SaveChanges();
                //await _unitOfWork.CommitAsync();
                return orderResponse;

            }
        }

        public async Task DeleteProductFromShoppingCart(Guid productDetailId)
        {
            var item = await _shoppingCartItemRepository.GetByIdAsync(productDetailId);
            if (item != null)
            {
                await _shoppingCartItemRepository.Delete(item);
            }
        }

        public async Task UpdateQuantityProductOnShoppingCart(Guid productDetailId, int quantity)
        {
            // kiểm tra sản phẩm có tồn tại trong giỏ hàng không
            if (await _shoppingCartItemRepository.IsHasCartItemAsync(productDetailId))
            {
                // cập nhật lại số lượng nếu số lượng lớn hơn 0
                if (quantity > 0)
                {
                    await _shoppingCartItemRepository.UpdateQuantityAsync(productDetailId, quantity);
                }
                else throw new BadRequestException(ErrorCode.Invalid, $"Số lượng sản phẩm phải lớn hơn không.");

            }
            else throw new BadRequestException(ErrorCode.Invalid, $"Sản phẩm không tồn tại trong giỏ hàng");
        }

        public async Task<OrderDTO> UpdateStatusOrderAsync(Guid orderId, OrderStatus orderStatus)
        {
            // kiểm tra order có tồn tại không
            var orderNeedUpdate = await _orderRepository.GetByIdAsync(orderId)
            ?? throw new BadRequestException(ErrorCode.Invalid, "Đơn hàng không tồn tại");

            orderNeedUpdate.OrderStatus = (int)orderStatus;

            var orderUpdated = await _orderRepository.UpdateAsync(orderNeedUpdate);
            return _mapper.Map<OrderDTO>(orderUpdated);
        }
    }
}
