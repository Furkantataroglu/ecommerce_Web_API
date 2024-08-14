using Entities.Concrete;
using Shared.Utilities_araçlar_.Results;
using Shared.Utilities_araçlar_.Results.Abstract_interfaces_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface ICartService
    {
        Task<IDataResult<Cart>> GetCartByUserIdAsync(Guid userId);
        Task<IResult> AddItemToCartAsync(Guid userId, Guid productId, int quantity);
        Task<IResult> RemoveItemFromCartAsync(Guid userId, Guid productId);
        Task<IResult> UpdateItemQuantityAsync(Guid userId, Guid productId, int quantity);
        Task<IResult> ClearCartAsync(Guid userId);
    }
}
