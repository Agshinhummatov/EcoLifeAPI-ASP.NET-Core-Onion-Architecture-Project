using AutoMapper;
using Repository.Repositories.Interfaces;
using Services.DTOs.Basket;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _repo;
        private readonly IMapper _mapper;
        public BasketService(IBasketRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task AddBasketAsync(int id)
        {
            await _repo.AddBasketAsync(id);
        }

        public async Task DeleteBasketAsync(int id)
        {
            await _repo.DeleteBasket(id);
        }


        public async Task DeleteBasketItemAsync(int id)
        {
            await _repo.DeleteItemBasket(id);
        }

        public async Task<int> GetBasketCountAsync()
        {
            int basketCount = await _repo.GetBasketCount();

            return basketCount;
        }


        public async Task<int> GetItemBasketCount(int id)
        {
            int basketCount = await _repo.GetProductQuantity(id);

            return basketCount;
        }



        public async Task<List<BasketProductListDto>> GetBasketProductsAsync()
        {
            var basketProducts = await _repo.GetBasketProducts();

            return _mapper.Map<List<BasketProductListDto>>(basketProducts);
        }
    }
}
