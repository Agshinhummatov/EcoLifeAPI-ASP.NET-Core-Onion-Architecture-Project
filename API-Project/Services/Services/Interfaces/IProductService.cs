using Domain.Models;
using Services.DTOs.Benefit;
using Services.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IProductService
    {

        Task<IEnumerable<ProductListDto>> GetAllAsync();
        Task<ProductListDto> GetByIdAsync(int? id);
        Task CreateAsync(ProductCreateDto product);
        Task DeleteAsync(int? id);
        Task UpdateAsync(int? id, ProductUpdateDto product);
        Task<IEnumerable<ProductListDto>> SearchAsync(string? searchText);
        Task SoftDeleteAsync(int? id);

        Task<List<Product>> GetProductCategory(int categorId);

        Task<int> GetCategoryProductCount(int categoryId);


    }
}
