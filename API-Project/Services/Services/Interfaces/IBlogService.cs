using Services.DTOs.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogListDto>> GetAllAsync();
        Task<BlogListDto> GetByIdAsync(int? id);
        Task CreateAsync(BlogCreateDto blogCreateDto);
        Task DeleteAsync(int? id);
        Task UpdateAsync(int? id, BlogUpdateDto blogUpdateDto);
        Task<IEnumerable<BlogListDto>> SearchAsync(string? searchText);
        Task SoftDeleteAsync(int? id);
    }
}
