using Services.DTOs.Advertising;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IAdvertisingService
    {
        Task<IEnumerable<AdvertisingListDto>> GetAllAsync();
        Task<AdvertisingListDto> GetByIdAsync(int? id);
        Task CreateAsync(AdvertisingCreateDto advertisingCreateDto);
        Task DeleteAsync(int? id);
        Task UpdateAsync(int? id, AdvertisingUpdateDto advertisingUpdateDto);
        Task<IEnumerable<AdvertisingListDto>> SearchAsync(string? searchText);
        Task SoftDeleteAsync(int? id);
    }
}
