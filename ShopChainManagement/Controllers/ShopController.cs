using Microsoft.AspNetCore.Mvc;
using ShopChainManagement.Models;
using ShopChainManagement.Services;

namespace ShopChainManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Shop>> GetShopByIdAsync(int id)
        {
            var shop = await _shopService.GetShopByIdAsync(id);
            if (shop == null)
            {
                return NotFound();
            }

            return shop;
        }

        [HttpGet("GetAllShopsAsync")]
        public async Task<IEnumerable<Shop>> GetAllShopsAsync()
        {
            return await _shopService.GetAllShopsAsync();
        }

        [HttpPost("CreateShopAsync")]
        public async Task<ActionResult<Product>> CreateShopAsync(Shop shop)
        {
            await _shopService.CreateShopAsync(shop);
            return CreatedAtAction(nameof(GetShopByIdAsync), new { id = shop.Id }, shop);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShopAsync(int id, Shop shop)
        {
            if (id != shop.Id)
            {
                return BadRequest();
            }

            await _shopService.UpdateShopAsync(shop);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShopAsync(int id)
        {
            var shop = await _shopService.GetShopByIdAsync(id);

            if (shop == null)
            {
                return NotFound();
            }

            await _shopService.DeleteShopAsync(id);

            return NoContent();
        }
    }

}