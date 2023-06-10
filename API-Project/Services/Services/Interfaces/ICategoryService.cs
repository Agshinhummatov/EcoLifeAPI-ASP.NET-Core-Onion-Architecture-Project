using Services.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryListDto>> GetAllAsync();
        Task<CategoryListDto> GetByIdAsync(int? id);
        Task CreateAsync(CategoryCreateDto category);
        Task DeleteAsync(int? id);
        Task UpdateAsync(int? id, CategoryUpdateDto category);
        Task<IEnumerable<CategoryListDto>> SearchAsync(string? searchText);
        Task SoftDeleteAsync(int? id);

    }
}
