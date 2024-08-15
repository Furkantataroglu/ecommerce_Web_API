using AutoMapper;
using DAL_DataAccessLayer.Abstarct;
using Entities.Concrete;
using Entities.Dtos;
using Services.Abstract;
using Shared.Utilities_araçlar_.Concrete;
using Shared.Utilities_araçlar_.Results;
using Shared.Utilities_araçlar_.Results.Abstract_interfaces_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class CartServiceManager : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> AddItemToCartAsync(Guid userId, Guid productId, int quantity)
        {
            var cart = await _unitOfWork.Carts.GetAsync(c => c.UserId == userId && c.Status == "Active");
            if (cart == null)
            {
                return new Result(ResultStatus.Error, "Active cart not found for user.");
            }

            var product = await _unitOfWork.Products.GetAsync(p => p.Id == productId);
            if (product == null)
            {
                return new Result(ResultStatus.Error, "Product not found.");
            }

            var existingCartItem = await _unitOfWork.CartItem.GetAsync(ci => ci.CartId == cart.Id && ci.ProductId == productId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
                existingCartItem.Price = (decimal)product.Price;
                await _unitOfWork.CartItem.UpdateAsync(existingCartItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity,
                    Price = (decimal)product.Price
                };
                await _unitOfWork.CartItem.AddAsync(cartItem);
            }

            cart.TotalPrice = await _unitOfWork.CartItem.GetAllAsync(ci => ci.CartId == cart.Id)
                .SumAsync(ci => ci.Quantity * ci.Price);

            await _unitOfWork.Carts.UpdateAsync(cart);
            await _unitOfWork.SaveAsync();

            return new Result(ResultStatus.Success, "Item added to cart successfully.");
        }

        public async Task<IResult> ClearCartAsync(Guid userId)
        {
            var cart = await _unitOfWork.Carts.GetAsync(c => c.UserId == userId && c.Status == "Active");
            if (cart == null)
            {
                return new Result(ResultStatus.Error, "Active cart not found for user.");
            }

            var cartItems = await _unitOfWork.CartItem.GetAllAsync(ci => ci.CartId == cart.Id);
            _unitOfWork.CartItem.RemoveRange(cartItems);

            cart.TotalPrice = 0;
            await _unitOfWork.Carts.UpdateAsync(cart);
            await _unitOfWork.SaveAsync();

            return new Result(ResultStatus.Success, "Cart cleared successfully.");
        }

        public async Task<IDataResult<CartDto>> GetCartByUserIdAsync(Guid userId)
        {
            var cart = await _unitOfWork.Carts.GetAsync(c => c.UserId == userId && c.Status == "Active", include: c => c.Include(c => c.CartItems).ThenInclude(ci => ci.Product));
            if (cart == null)
            {
                return new DataResult<CartDto>(ResultStatus.Error, "Active cart not found for user.", null);
            }

            var cartDto = _mapper.Map<CartDto>(cart);
            return new DataResult<CartDto>(ResultStatus.Success, "Cart retrieved successfully.", cartDto);
        }

        public async Task<IResult> RemoveItemFromCartAsync(Guid userId, Guid productId)
        {
            var cart = await _unitOfWork.Carts.GetAsync(c => c.UserId == userId && c.Status == "Active");
            if (cart == null)
            {
                return new Result(ResultStatus.Error, "Active cart not found for user.");
            }

            var cartItem = await _unitOfWork.CartItem.GetAsync(ci => ci.CartId == cart.Id && ci.ProductId == productId);
            if (cartItem == null)
            {
                return new Result(ResultStatus.Error, "Cart item not found.");
            }

            await _unitOfWork.CartItem.DeleteAsync(cartItem);

            cart.TotalPrice = await _unitOfWork.CartItems.GetAllAsync(ci => ci.CartId == cart.Id)
                .SumAsync(ci => ci.Quantity * ci.Price);

            await _unitOfWork.Carts.UpdateAsync(cart);
            await _unitOfWork.SaveAsync();

            return new Result(ResultStatus.Success, "Item removed from cart successfully.");
        }

        public async Task<IResult> UpdateItemQuantityAsync(Guid userId, Guid productId, int quantity)
        {
            var cart = await _unitOfWork.Carts.GetAsync(c => c.UserId == userId && c.Status == "Active");
            if (cart == null)
            {
                return new Result(ResultStatus.Error, "Active cart not found for user.");
            }

            var cartItem = await _unitOfWork.CartItem.GetAsync(ci => ci.CartId == cart.Id && ci.ProductId == productId);
            if (cartItem == null)
            {
                return new Result(ResultStatus.Error, "Cart item not found.");
            }

            cartItem.Quantity = quantity;
            cartItem.Price = await _unitOfWork.Products.GetAsync(p => p.Id == productId).ContinueWith(t => t.Result.Price ?? 0);
            await _unitOfWork.CartItem.UpdateAsync(cartItem);

            cart.TotalPrice = await _unitOfWork.CartItems.GetAllAsync(ci => ci.CartId == cart.Id)
                .SumAsync(ci => ci.Quantity * ci.Price);

            await _unitOfWork.Carts.UpdateAsync(cart);
            await _unitOfWork.SaveAsync();

            return new Result(ResultStatus.Success, "Cart item quantity updated successfully.");
        }
    }
}
