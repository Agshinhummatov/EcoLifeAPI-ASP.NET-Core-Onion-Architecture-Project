
using Services.DTOs.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<SliderListDto>> GetAllAsync();
        Task<SliderListDto> GetByIdAsync(int? id);
        Task CreateAsync(SliderCreateDto sliderCreateDto);
        Task DeleteAsync(int? id);
        Task UpdateAsync(int? id, SliderUpdateDto sliderUpdateDto);
        Task<IEnumerable<SliderListDto>> SearchAsync(string? searchText);
        Task SoftDeleteAsync(int? id);
    }
}
