using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.Advertising;
using Services.DTOs.Banner;
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
    public class AdvertisingService :IAdvertisingService
    {
        private readonly IAdvertisingRepository _adverstingRepo;
        private readonly IMapper _mapper;

        public AdvertisingService(IAdvertisingRepository adverstingRepo, IMapper mapper)
        {
            _adverstingRepo = adverstingRepo;
            _mapper = mapper;
        }


        public async Task CreateAsync(AdvertisingCreateDto advertisingCreateDto)
        {

            var validationContext = new ValidationContext(advertisingCreateDto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(advertisingCreateDto, validationContext, validationResults, true);

            if (!isValid)
            {
                string errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new Exception(errorMessages);
            }

            if (string.IsNullOrEmpty(advertisingCreateDto.Title) || string.IsNullOrEmpty(advertisingCreateDto.Description))
            {
                throw new Exception("Title and Description are required.");
            }

            var mapAdvertising = _mapper.Map<Advertising>(advertisingCreateDto);
            mapAdvertising.Image = await advertisingCreateDto.Photo.GetBytes();
            await _adverstingRepo.CreateAsync(mapAdvertising);
           

        }

        public async Task<IEnumerable<AdvertisingListDto>> GetAllAsync() => _mapper.Map<IEnumerable<AdvertisingListDto>>(await _adverstingRepo.FindAllAsync());

        public async Task<AdvertisingListDto> GetByIdAsync(int? id) => _mapper.Map<AdvertisingListDto>(await _adverstingRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _adverstingRepo.DeleteAsync(await _adverstingRepo.GetByIdAsync(id));

        public async Task UpdateAsync(int? id, AdvertisingUpdateDto advertisingUpdateDto)
        {

            if (id is null) throw new ArgumentNullException();

            var existAdvertising = await _adverstingRepo.GetByIdAsync(id) ?? throw new NullReferenceException();

            existAdvertising.Title = advertisingUpdateDto.Title ?? existAdvertising.Title;
            existAdvertising.Description = advertisingUpdateDto.Description ?? existAdvertising.Description;

            if (advertisingUpdateDto.Photo != null)
            {

                existAdvertising.Image = await advertisingUpdateDto.Photo.GetBytes();
            }

            await _adverstingRepo.UpdateAsync(existAdvertising);
        }

        public async Task<IEnumerable<AdvertisingListDto>> SearchAsync(string? searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return _mapper.Map<IEnumerable<AdvertisingListDto>>(await _adverstingRepo.FindAllAsync());
            return _mapper.Map<IEnumerable<AdvertisingListDto>>(await _adverstingRepo.FindAllAsync(m => m.Title.Contains(searchText)));
        }

        public async Task SoftDeleteAsync(int? id)
        {
            await _adverstingRepo.SoftDeleteAsync(id);
        }

    }
}
