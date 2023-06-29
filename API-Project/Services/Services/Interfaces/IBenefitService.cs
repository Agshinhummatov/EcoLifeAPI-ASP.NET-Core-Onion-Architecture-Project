using Services.DTOs.Benefit;
using Services.DTOs.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public  interface IBenefitService
    {
        Task<IEnumerable<BenefitListDto>> GetAllAsync();
        Task<BenefitListDto> GetByIdAsync(int? id);
        Task CreateAsync(BenefitCreateDto benefitCreateDto);
        Task DeleteAsync(int? id);
        Task UpdateAsync(int? id, BenefitUpdateDto benefitUpdateDto);
        Task<IEnumerable<BenefitListDto>> SearchAsync(string? searchText);
        Task SoftDeleteAsync(int? id);
    }
}
