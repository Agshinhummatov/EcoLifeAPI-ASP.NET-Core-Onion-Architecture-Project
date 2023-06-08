using AutoMapper;
using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Services.DTOs.Benefit;

using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BenefitService : IBenefitService
    {
        private readonly IBenefitRepository _benefitRepo;
        private readonly IMapper _mapper;
        public BenefitService(IBenefitRepository benefitRepo, IMapper mapper)
        {
            _benefitRepo = benefitRepo;
            _mapper = mapper;
        }


        public async Task CreateAsync(BenefitCreateDto benefit) => await _benefitRepo.CreateAsync(_mapper.Map<Benefit>(benefit));

        public async Task<IEnumerable<BenefitListDto>> GetAllAsync() => _mapper.Map<IEnumerable<BenefitListDto>>(await _benefitRepo.FindAllAsync());

        public async Task<BenefitListDto> GetByIdAsync(int? id) => _mapper.Map<BenefitListDto>(await _benefitRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _benefitRepo.DeleteAsync(await _benefitRepo.GetByIdAsync(id));

        public async Task UpdateAsync(int? id, BenefitUpdateDto benefit)
        {
            if (id is null) throw new ArgumentNullException();

            var existBenefit = await _benefitRepo.GetByIdAsync(id) ?? throw new NullReferenceException();

            _mapper.Map(benefit, existBenefit);

            await _benefitRepo.UpdateAsync(existBenefit);
        }

        public async Task<IEnumerable<BenefitListDto>> SearchAsync(string? searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return _mapper.Map<IEnumerable<BenefitListDto>>(await _benefitRepo.FindAllAsync());
            return _mapper.Map<IEnumerable<BenefitListDto>>(await _benefitRepo.FindAllAsync(m => m.Title.Contains(searchText)));
        }

        public async Task SoftDeleteAsync(int? id)
        {
            await _benefitRepo.SoftDeleteAsync(id);
        }


    }
}
