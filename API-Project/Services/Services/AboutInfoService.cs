using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.AboutInfo;
using Services.DTOs.Advertising;
using Services.DTOs.Slider;
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
    public class AboutInfoService:IAboutInfoService
    {
        private IAboutInfoRepository _aboutRepo;
        private readonly IMapper _mapper;

        public AboutInfoService(IAboutInfoRepository aboutRepo, IMapper mapper)
        {
            _aboutRepo = aboutRepo;
            _mapper = mapper;
        }

        public async Task CreateAsync(AboutInfoCreateDto aboutInfoCreateDto)
        {
            var validationContext = new ValidationContext(aboutInfoCreateDto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(aboutInfoCreateDto, validationContext, validationResults, true);

            if (!isValid)
            {
                string errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new Exception(errorMessages);
            }

            if (string.IsNullOrEmpty(aboutInfoCreateDto.Title) || string.IsNullOrEmpty(aboutInfoCreateDto.Description))
            {
                throw new Exception("Title and Description are required.");
            }

            var mapAboutInfo = _mapper.Map<AboutInfo>(aboutInfoCreateDto);
            mapAboutInfo.Image = await aboutInfoCreateDto.Photo.GetBytes();
            await _aboutRepo.CreateAsync(mapAboutInfo);
        }

        public async Task<IEnumerable<AboutInfoListDto>> GetAllAsync() => _mapper.Map<IEnumerable<AboutInfoListDto>>(await _aboutRepo.FindAllAsync());

        public async Task<AboutInfoListDto> GetByIdAsync(int? id) => _mapper.Map<AboutInfoListDto>(await _aboutRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _aboutRepo.DeleteAsync(await _aboutRepo.GetByIdAsync(id));

        public async Task UpdateAsync(int? id, AboutInfoUpdateDto aboutInfoUpdateDto)
        {
            if (id is null) throw new ArgumentNullException();

            var existAdvertising = await _aboutRepo.GetByIdAsync(id) ?? throw new NullReferenceException();

            existAdvertising.Title = aboutInfoUpdateDto.Title ?? existAdvertising.Title;
            existAdvertising.Description = aboutInfoUpdateDto.Description ?? existAdvertising.Description;

            if (aboutInfoUpdateDto.Photo != null)
            {

                existAdvertising.Image = await aboutInfoUpdateDto.Photo.GetBytes();
            }

            await _aboutRepo.UpdateAsync(existAdvertising);
        }

        public async Task<IEnumerable<AboutInfoListDto>> SearchAsync(string? searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return _mapper.Map<IEnumerable<AboutInfoListDto>>(await _aboutRepo.FindAllAsync());
            return _mapper.Map<IEnumerable<AboutInfoListDto>>(await _aboutRepo.FindAllAsync(m => m.Title.Contains(searchText)));
        }

        public async Task SoftDeleteAsync(int? id)
        {
            await _aboutRepo.SoftDeleteAsync(id);
        }
    }
}
