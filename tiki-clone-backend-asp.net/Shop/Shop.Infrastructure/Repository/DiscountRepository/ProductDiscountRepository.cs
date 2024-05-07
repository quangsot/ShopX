using Shop.Application.UnitOfWork;
using Shop.Domain.Entity;
using Shop.Domain.Interface.Repository.DiscountRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repository.DiscountRepository
{
    public class ProductDiscountRepository : WriteRepository<Productdiscount>, IProductDiscountRepository
    {
        public ProductDiscountRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
