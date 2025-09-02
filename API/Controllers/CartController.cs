using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using Shared.Utilities_araçlar_.Results;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly UserManager<User> _userManager;

        public CartController(ICartService cartService, UserManager<User> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        /// <summary>
        /// Kullanıcının sepetini getirir
        /// </summary>
        [HttpGet("my-cart")]
        public async Task<IActionResult> GetMyCart()
        {
            var userId = await GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized("User not authenticated");
            }

            var result = await _cartService.GetCartByUserIdAsync(userId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Sepete ürün ekler
        /// </summary>
        [HttpPost("add-item")]
        public async Task<IActionResult> AddItemToCart([FromBody] CartItemAddDto cartItemAddDto)
        {
            var userId = await GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized("User not authenticated");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _cartService.AddItemToCartAsync(userId, cartItemAddDto.ProductId, cartItemAddDto.Quantity);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Sepetten ürün çıkarır
        /// </summary>
        [HttpDelete("remove-item/{productId}")]
        public async Task<IActionResult> RemoveItemFromCart(Guid productId)
        {
            var userId = await GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized("User not authenticated");
            }

            var result = await _cartService.RemoveItemFromCartAsync(userId, productId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Sepetteki ürün miktarını günceller
        /// </summary>
        [HttpPut("update-quantity")]
        public async Task<IActionResult> UpdateItemQuantity([FromBody] CartItemAddDto cartItemUpdateDto)
        {
            var userId = await GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized("User not authenticated");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _cartService.UpdateItemQuantityAsync(userId, cartItemUpdateDto.ProductId, cartItemUpdateDto.Quantity);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Sepeti tamamen temizler
        /// </summary>
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = await GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized("User not authenticated");
            }

            var result = await _cartService.ClearCartAsync(userId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Yetersiz stok olan ürünleri sepetten çıkarır
        /// </summary>
        [HttpDelete("remove-insufficient-stock")]
        public async Task<IActionResult> RemoveInsufficientStockItems()
        {
            var userId = await GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized("User not authenticated");
            }

            var result = await _cartService.RemoveInsufficientStockItemsAsync(userId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Mevcut kullanıcının ID'sini JWT token'dan alır
        /// </summary>
        private async Task<Guid> GetCurrentUserId()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return Guid.Empty;
            }

            var user = await _userManager.FindByEmailAsync(userEmail);
            return user?.Id ?? Guid.Empty;
        }
    }
}
