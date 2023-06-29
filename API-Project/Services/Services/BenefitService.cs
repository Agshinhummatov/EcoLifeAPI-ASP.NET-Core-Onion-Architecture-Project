using AutoMapper;
using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Services.DTOs.Advertising;
using Services.DTOs.Benefit;
using Services.Helpers;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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


        public async Task CreateAsync(BenefitCreateDto benefitCreateDto)
        {
            var validationContext = new ValidationContext(benefitCreateDto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(benefitCreateDto, validationContext, validationResults, true);

            if (!isValid)
            {
                string errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new Exception(errorMessages);
            }

            if (string.IsNullOrEmpty(benefitCreateDto.Title))
            {
                throw new Exception("Title is required.");
            }

            var mapBenefit = _mapper.Map<Benefit>(benefitCreateDto);
            mapBenefit.Image = await benefitCreateDto.Photo.GetBytes();
            await _benefitRepo.CreateAsync(mapBenefit);

        }

        public async Task<IEnumerable<BenefitListDto>> GetAllAsync() => _mapper.Map<IEnumerable<BenefitListDto>>(await _benefitRepo.FindAllAsync());

        public async Task<BenefitListDto> GetByIdAsync(int? id) => _mapper.Map<BenefitListDto>(await _benefitRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _benefitRepo.DeleteAsync(await _benefitRepo.GetByIdAsync(id));

        public async Task UpdateAsync(int? id, BenefitUpdateDto benefitUpdateDto)
        {

            if (id is null) throw new ArgumentNullException();

            var existBenefit = await _benefitRepo.GetByIdAsync(id) ?? throw new NullReferenceException();

            existBenefit.Title = benefitUpdateDto.Title ?? existBenefit.Title;
           

            if (benefitUpdateDto.Photo != null)
            {

                existBenefit.Image = await benefitUpdateDto.Photo.GetBytes();
            }

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
