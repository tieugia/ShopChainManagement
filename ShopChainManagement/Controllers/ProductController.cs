using Microsoft.AspNetCore.Mvc;
using ShopChainManagement.Models;
using ShopChainManagement.Services;

namespace ShopChainManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProductsAsync")]
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productService.SearchProductAsync(string.Empty);
        }

        [HttpGet("GetProductsBySearchTermAsync")]
        public async Task<IEnumerable<Product>> GetProductsBySearchTermAsync(string? searchTerm = null)
        {
            return await _productService.SearchProductAsync(searchTerm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost("CreateProductAsync")]
        public async Task<ActionResult<Product>> CreateProductAsync(Product Product)
        {
            await _productService.CreateProductAsync(Product);
            return CreatedAtAction(nameof(GetProductByIdAsync), new { id = Product.Id }, Product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(int id, Product Product)
        {
            if (id != Product.Id)
            {
                return BadRequest();
            }

            await _productService.UpdateProductAsync(Product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductAsync(id);

            return NoContent();
        }
    }
}