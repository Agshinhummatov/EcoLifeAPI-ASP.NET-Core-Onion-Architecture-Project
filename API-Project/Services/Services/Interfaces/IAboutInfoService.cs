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
        Task CreateAsync(AboutInfoCreateDto about);
        Task DeleteAsync(int? id);
        Task UpdateAsync(int? id, AboutInfoUpdateDto about);
        Task<IEnumerable<AboutInfoListDto>> SearchAsync(string? searchText);
        Task SoftDeleteAsync(int? id);
    }
}
