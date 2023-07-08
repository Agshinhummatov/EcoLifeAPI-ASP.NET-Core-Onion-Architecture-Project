using AutoMapper;
using Repository.Repositories.Interfaces;
using Services.DTOs.Basket;
using Services.DTOs.Wishlist;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _repo;
        private readonly IMapper _mapper;
        public WishlistService(IWishlistRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task AddWishlistAsync(int id)
        {
            await _repo.AddWishlistAsync(id);
        }

        public async Task DeleteWishlistAsync(int id)
        {
            await _repo.DeleteWishlist(id);
        }


        public async Task<int> GetWishlistCountAsync()
        {
            int basketCount = await _repo.GetWishlistCount();

            return basketCount;
        }


        public async Task<List<WishlistProductListDto>> GetWishlistProductsAsync()
        {
            var basketProducts = await _repo.GetWishlistProducts();

            return _mapper.Map<List<WishlistProductListDto>>(basketProducts);
        }

       
    }
}
