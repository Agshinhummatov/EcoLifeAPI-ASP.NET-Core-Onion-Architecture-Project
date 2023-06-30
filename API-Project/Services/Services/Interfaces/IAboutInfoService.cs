using Services.DTOs.AboutInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IAboutInfoService
    {
        Task<IEnumerable<AboutInfoListDto>> GetAllAsync();
        Task<AboutInfoListDto> GetByIdAsync(int? id);
        Task CreateAsync(AboutInfoCreateDto aboutInfoCreateDto);
        Task DeleteAsync(int? id);
        Task UpdateAsync(int? id, AboutInfoUpdateDto aboutInfoUpdateDto);
        Task<IEnumerable<AboutInfoListDto>> SearchAsync(string? searchText);
        Task SoftDeleteAsync(int? id);
    }
}
