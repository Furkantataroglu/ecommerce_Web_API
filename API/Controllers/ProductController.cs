using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using Shared.Utilities_araçlar_.Results;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAll();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _productService.Get(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
            
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ProductAddDto productAddDto)
        {
            var result = await _productService.Add(productAddDto);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return CreatedAtAction(nameof(Get), new { id = result.Data.Id }, result.Data);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            if (id != productUpdateDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _productService.Update(productUpdateDto);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productService.Delete(id);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return NoContent();
            }
            return BadRequest(result);
        }

        // GET: api/products/{id}/calculateStockValue
        [HttpGet("{id}/calculateStockValue")]
        public async Task<IActionResult> CalculateStockValue(Guid id)
        {
            var productResult = await _productService.Get(id);
            if (productResult.ResultStatus != ResultStatus.Success)
            {
                return NotFound(productResult);
            }

            var stockValue = _productService.CalculateStockValue(productResult.Data);
            return Ok(new { StockValue = stockValue });
        }
    }
}
