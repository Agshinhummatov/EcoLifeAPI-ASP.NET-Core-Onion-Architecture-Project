using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;

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
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepo;
        private readonly IMapper _mapper;

        public SliderService(ISliderRepository sliderRepo, IMapper mapper)
        {
            _sliderRepo = sliderRepo;
            _mapper = mapper;
        }

        public async Task CreateAsync(SliderCreateDto sliderCreateDto)
        {
            var validationContext = new ValidationContext(sliderCreateDto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(sliderCreateDto, validationContext, validationResults, true);

            if (!isValid)
            {
                string errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new Exception(errorMessages);
            }

            if (string.IsNullOrEmpty(sliderCreateDto.Title) || string.IsNullOrEmpty(sliderCreateDto.Description))
            {
                throw new Exception("Title and Description are required.");
            }



            var mapSlider = _mapper.Map<Slider>(sliderCreateDto);
            mapSlider.Image = await sliderCreateDto.Photo.GetBytes();
            await _sliderRepo.CreateAsync(mapSlider);
        }

        public async Task<IEnumerable<SliderListDto>> GetAllAsync() => _mapper.Map<IEnumerable<SliderListDto>>(await _sliderRepo.FindAllAsync());

        public async Task<SliderListDto> GetByIdAsync(int? id) => _mapper.Map<SliderListDto>(await _sliderRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _sliderRepo.DeleteAsync(await _sliderRepo.GetByIdAsync(id));

        public async Task UpdateAsync(int? id, SliderUpdateDto sliderUpdateDto)
        {
            if (id is null) throw new ArgumentNullException();

            var existSlider = await _sliderRepo.GetByIdAsync(id) ?? throw new NullReferenceException();

            existSlider.Title = sliderUpdateDto.Title ?? existSlider.Title;
            existSlider.Description = sliderUpdateDto.Description ?? existSlider.Description;

            if (sliderUpdateDto.Photo != null)
            {

                existSlider.Image = await sliderUpdateDto.Photo.GetBytes();
            }

            await _sliderRepo.UpdateAsync(existSlider);
        }

        public async Task<IEnumerable<SliderListDto>> SearchAsync(string? searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return _mapper.Map<IEnumerable<SliderListDto>>(await _sliderRepo.FindAllAsync());
            return _mapper.Map<IEnumerable<SliderListDto>>(await _sliderRepo.FindAllAsync(m => m.Title.Contains(searchText)));
        }

        public async Task SoftDeleteAsync(int? id)
        {
            await _sliderRepo.SoftDeleteAsync(id);
        }
    }
}
