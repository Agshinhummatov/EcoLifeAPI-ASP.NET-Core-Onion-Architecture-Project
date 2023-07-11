using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddWishlist([Required][FromQuery] int id)
        {

            await _wishlistService.AddWishlistAsync(id);
            return Ok();
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetWishlistProducts()
        {

            return Ok(await _wishlistService.GetWishlistProductsAsync());
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetWishlistCount()
        {
            try
            {
                var basketCount = await _wishlistService.GetWishlistCountAsync();
                return Ok(basketCount);
            }
            catch (Exception ex)
            {
                // Hata durumunda yapılacak işlemler
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }



        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteWishlistProduct([Required][FromQuery] int id)
        {
            await _wishlistService.DeleteWishlistAsync(id);
            return Ok();
        }

    }
}
