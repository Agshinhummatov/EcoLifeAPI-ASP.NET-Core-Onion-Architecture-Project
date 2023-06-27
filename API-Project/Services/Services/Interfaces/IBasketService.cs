using Services.DTOs.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IBasketService
    {
        Task AddBasketAsync(int id);
        Task<List<BasketProductListDto>> GetBasketProductsAsync();
        Task DeleteBasketAsync(int id);
        Task<int> GetBasketCountAsync();

        Task DeleteBasketItemAsync(int id);

        Task<int> GetItemBasketCount(int id);

    }
}
