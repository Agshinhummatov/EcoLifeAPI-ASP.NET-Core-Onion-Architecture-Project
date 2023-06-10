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


       
        [HttpPost]
        public async Task<IActionResult> AddBasket([Required][FromQuery] int id)
        {

            await _basketService.AddBasketAsync(id);
            return Ok();
        }


     
        [HttpGet]
        public async Task<IActionResult> GetBasketProducts()
        {

            return Ok(await _basketService.GetBasketProductsAsync());
        }



        [HttpGet]
        public async Task<IActionResult> GetBasketCount()
        {
            return Ok(await _basketService.GetBasketCountAsync());
        }


  
        [HttpDelete]
        public async Task<IActionResult> DeleteBasketProduct([Required][FromQuery] int id)
        {
            await _basketService.DeleteBasketAsync(id);
            return Ok();
        }
    }
}
