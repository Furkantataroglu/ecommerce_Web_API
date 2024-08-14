using DAL_DataAccessLayer.EntityFramework.Contexts;
using Entities.Concrete;
using Services.Abstract;
using Shared.Utilities_araçlar_.Results;
using Shared.Utilities_araçlar_.Results.Abstract_interfaces_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class CartServiceManager : ICartService
    {
        private readonly MyDbContext _context;

        public CartServiceManager(MyDbContext context)
        {
            _context = context;
        }
        public Task<IResult> AddItemToCartAsync(Guid userId, Guid productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> ClearCartAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<Cart>> GetCartByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> RemoveItemFromCartAsync(Guid userId, Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateItemQuantityAsync(Guid userId, Guid productId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
