using Shop.Application.UnitOfWork;
using Shop.Domain.Entity;
using Shop.Domain.Interface.Repository.OrdersRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repository.OrdersRepository
{
    public class OrderItemRepository : WriteRepository<Orderitem>, IOrderItemRepository
    {
        public OrderItemRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
