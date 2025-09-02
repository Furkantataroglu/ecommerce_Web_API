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
using Microsoft.EntityFrameworkCore;

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
                // Cart yoksa otomatik oluştur
                cart = new Cart
                {
                    UserId = userId,
                    Status = "Active",
                    TotalPrice = 0
                };
                await _unitOfWork.Carts.AddAsync(cart);
                await _unitOfWork.SaveAsync();
            }

            var product = await _unitOfWork.Products.GetAsync(p => p.Id == productId);
            if (product == null)
            {
                return new Result(ResultStatus.Error, "Product not found.");
            }

            // Stok kontrolü - ürün aktif mi?
            if (!product.IsActive)
            {
                return new Result(ResultStatus.Error, "Product is not active.");
            }

            var existingCartItem = await _unitOfWork.CartItems.GetAsync(ci => ci.CartId == cart.Id && ci.ProductId == productId);
            if (existingCartItem != null)
            {
                // Mevcut ürün + yeni miktar stoktan fazla mı kontrol et
                var totalRequestedQuantity = existingCartItem.Quantity + quantity;
                if (product.StockQuantity < totalRequestedQuantity)
                {
                    return new Result(ResultStatus.Error, $"Insufficient stock for total quantity. Available: {product.StockQuantity}, Current in cart: {existingCartItem.Quantity}, Additional requested: {quantity}, Total would be: {totalRequestedQuantity}");
                }

                existingCartItem.Quantity += quantity;
                existingCartItem.Price = (decimal)product.Price;
                await _unitOfWork.CartItems.UpdateAsync(existingCartItem);
            }
            else
            {
                // Yeni ürün eklerken stok kontrolü
                if (product.StockQuantity < quantity)
                {
                    return new Result(ResultStatus.Error, $"Insufficient stock. Available: {product.StockQuantity}, Requested: {quantity}");
                }

                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity,
                    Price = (decimal)product.Price
                };
                await _unitOfWork.CartItems.AddAsync(cartItem);
            }

            var cartItems = await _unitOfWork.CartItems.GetAllAsync(ci => ci.CartId == cart.Id);
            cart.TotalPrice = cartItems.Sum(ci => ci.Quantity * ci.Price);

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

            var cartItems = await _unitOfWork.CartItems.GetAllAsync(ci => ci.CartId == cart.Id);
            await _unitOfWork.CartItems.RemoveRangeAsync(cartItems);

            cart.TotalPrice = 0;
            await _unitOfWork.Carts.UpdateAsync(cart);
            await _unitOfWork.SaveAsync();

            return new Result(ResultStatus.Success, "Cart cleared successfully.");
        }

        public async Task<IDataResult<CartDto>> GetCartByUserIdAsync(Guid userId)
        {
            var cart = await _unitOfWork.Carts.GetWithIncludesAsync(
                c => c.UserId == userId && c.Status == "Active",
                query => query.Include(c => c.CartItems).ThenInclude(ci => ci.Product));
                
            if (cart == null)
            {
                // Cart yoksa otomatik oluştur
                cart = new Cart
                {
                    UserId = userId,
                    Status = "Active",
                    TotalPrice = 0
                };
                await _unitOfWork.Carts.AddAsync(cart);
                await _unitOfWork.SaveAsync();
            }

            var cartDto = _mapper.Map<CartDto>(cart);
            return new DataResult<CartDto>(ResultStatus.Success, "Cart retrieved successfully.", cartDto);
        }

        public async Task<IResult> RemoveItemFromCartAsync(Guid userId, Guid productId)
        {
            var cart = await _unitOfWork.Carts.GetAsync(c => c.UserId == userId && c.Status == "Active");
            if (cart == null)
            {
                // Cart yoksa otomatik oluştur
                cart = new Cart
                {
                    UserId = userId,
                    Status = "Active",
                    TotalPrice = 0
                };
                await _unitOfWork.Carts.AddAsync(cart);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, "Cart created successfully. No items to remove.");
            }

            var cartItem = await _unitOfWork.CartItems.GetAsync(ci => ci.CartId == cart.Id && ci.ProductId == productId);
            if (cartItem == null)
            {
                return new Result(ResultStatus.Error, "Cart item not found.");
            }

            await _unitOfWork.CartItems.DeleteAsync(cartItem);

            var cartItems = await _unitOfWork.CartItems.GetAllAsync(ci => ci.CartId == cart.Id);
            cart.TotalPrice = cartItems.Sum(ci => ci.Quantity * ci.Price);

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

            var cartItem = await _unitOfWork.CartItems.GetAsync(ci => ci.CartId == cart.Id && ci.ProductId == productId);
            if (cartItem == null)
            {
                return new Result(ResultStatus.Error, "Cart item not found.");
            }

            // Stok kontrolü
            var product = await _unitOfWork.Products.GetAsync(p => p.Id == productId);
            if (product == null)
            {
                return new Result(ResultStatus.Error, "Product not found.");
            }

            if (product.StockQuantity < quantity)
            {
                return new Result(ResultStatus.Error, $"Insufficient stock. Available: {product.StockQuantity}, Requested: {quantity}");
            }

            cartItem.Quantity = quantity;
            cartItem.Price = product?.Price ?? 0;
            await _unitOfWork.CartItems.UpdateAsync(cartItem);

            var cartItems = await _unitOfWork.CartItems.GetAllAsync(ci => ci.CartId == cart.Id);
            cart.TotalPrice = cartItems.Sum(ci => ci.Quantity * ci.Price);

            await _unitOfWork.Carts.UpdateAsync(cart);
            await _unitOfWork.SaveAsync();

            return new Result(ResultStatus.Success, "Cart item quantity updated successfully.");
        }

        /// <summary>
        /// Yetersiz stok olan ürünleri sepetten çıkarır
        /// </summary>
        public async Task<IResult> RemoveInsufficientStockItemsAsync(Guid userId)
        {
            var cart = await _unitOfWork.Carts.GetWithIncludesAsync(
                c => c.UserId == userId && c.Status == "Active",
                query => query.Include(c => c.CartItems).ThenInclude(ci => ci.Product));

            if (cart == null)
            {
                return new Result(ResultStatus.Error, "Active cart not found for user.");
            }

            var removedItems = new List<string>();
            var cartItemsToRemove = new List<CartItem>();

            foreach (var cartItem in cart.CartItems)
            {
                if (cartItem.Product != null && cartItem.Quantity > cartItem.Product.StockQuantity)
                {
                    cartItemsToRemove.Add(cartItem);
                    removedItems.Add($"{cartItem.Product.Name} (Requested: {cartItem.Quantity}, Available: {cartItem.Product.StockQuantity})");
                }
            }

            if (cartItemsToRemove.Any())
            {
                await _unitOfWork.CartItems.RemoveRangeAsync(cartItemsToRemove);
                
                // Toplam fiyatı güncelle
                var remainingCartItems = await _unitOfWork.CartItems.GetAllAsync(ci => ci.CartId == cart.Id);
                cart.TotalPrice = remainingCartItems.Sum(ci => ci.Quantity * ci.Price);
                
                await _unitOfWork.Carts.UpdateAsync(cart);
                await _unitOfWork.SaveAsync();

                var message = $"Removed {cartItemsToRemove.Count} items with insufficient stock: " + string.Join("; ", removedItems);
                return new Result(ResultStatus.Success, message);
            }

            return new Result(ResultStatus.Success, "No items with insufficient stock found.");
        }
    }
}
