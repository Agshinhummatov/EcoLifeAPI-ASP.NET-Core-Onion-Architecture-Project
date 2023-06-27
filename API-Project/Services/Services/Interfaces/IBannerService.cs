using Services.DTOs.Banner;
using Services.DTOs.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IBannerService
    {
        Task<IEnumerable<BannerListDto>> GetAllAsync();
        Task<BannerListDto> GetByIdAsync(int? id);
        Task CreateAsync(BannerCreateDto bannerCreateDto);
        Task DeleteAsync(int? id);
        Task UpdateAsync(int? id, BannerUpdateDto bannerUpdateDto);
        Task<IEnumerable<BannerListDto>> SearchAsync(string? searchText);
        Task SoftDeleteAsync(int? id);

    }
}
