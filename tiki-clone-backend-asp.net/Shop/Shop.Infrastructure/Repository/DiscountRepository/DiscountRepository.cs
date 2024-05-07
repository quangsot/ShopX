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
    public class DiscountRepository : WriteRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


    }
}
