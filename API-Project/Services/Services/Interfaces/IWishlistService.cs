using Services.DTOs.Basket;
using Services.DTOs.Wishlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IWishlistService
    {
        Task AddWishlistAsync(int id);
        Task<List<WishlistProductListDto>> GetWishlistProductsAsync();
        Task DeleteWishlistAsync(int id);

        Task<int> GetWishlistCountAsync();


    }
}
