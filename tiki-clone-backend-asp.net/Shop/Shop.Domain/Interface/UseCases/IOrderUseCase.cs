using Microsoft.AspNetCore.Http;
using Shop.Domain.Enum;
using Shop.Domain.Model.DTO;
using Shop.Domain.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interface.UseCases
{
    public interface IOrderUseCase
    {
        /// <summary>
        /// Thêm sản phẩm vào giỏ hàng
        /// </summary>
        /// <param name="productDetailId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task AddProductToShoppingCart(Guid productDetailId, int quantity, HttpContext httpContext);

        /// <summary>
        /// Xóa sản phẩm khỏi giỏ hàng
        /// </summary>
        /// <param name="productDetailId"></param>
        /// <returns></returns>
        Task DeleteProductFromShoppingCart(Guid productDetailId);

        /// <summary>
        /// Cập nhật số lượng sản phẩm trong giỏ hàng
        /// </summary>
        /// <param name="productDetailId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task UpdateQuantityProductOnShoppingCart(Guid productDetailId, int quantity);

        /// <summary>
        /// tạo mới đơn hàng
        /// </summary>
        /// <param name="orderResponse"></param>
        /// <returns></returns>
        Task<OrderResponse> CreateOrderAsync(OrderResponse orderResponse);

        /// <summary>
        /// cập nhật trạng thái của đơn hàng
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        Task<OrderDTO> UpdateStatusOrderAsync(Guid orderId, OrderStatus orderStatus);
    }
}
