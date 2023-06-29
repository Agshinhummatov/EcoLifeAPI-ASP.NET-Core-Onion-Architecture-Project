using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddBasket([Required][FromQuery] int id)
        {

            await _basketService.AddBasketAsync(id);
            return Ok();
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetBasketProducts()
        {

            return Ok(await _basketService.GetBasketProductsAsync());
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetBasketCount()
        {
            try
            {
                var basketCount = await _basketService.GetBasketCountAsync();
                return Ok(basketCount);
            }
            catch (Exception ex)
            {
                // Hata durumunda yapılacak işlemler
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }

        [Route("{id}")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetBasketItemCount([FromRoute][Required] int id)
        {
            return Ok(await _basketService.GetItemBasketCount(id));
        }


        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteBasketProduct([Required][FromQuery] int id)
        {
            await _basketService.DeleteBasketAsync(id);
            return Ok();
        }


        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteBasketItemProduct([Required][FromQuery] int id)
        {
            await _basketService.DeleteBasketItemAsync(id);
            return Ok();
        }
    }
}
