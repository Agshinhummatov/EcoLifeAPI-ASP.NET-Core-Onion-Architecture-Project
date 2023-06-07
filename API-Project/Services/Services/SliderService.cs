using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;

using Services.DTOs.Slider;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task CreateAsync(SliderCreateDto slider) => await _sliderRepo.CreateAsync(_mapper.Map<Slider>(slider));

        public async Task<IEnumerable<SliderListDto>> GetAllAsync() => _mapper.Map<IEnumerable<SliderListDto>>(await _sliderRepo.FindAllAsync());

        public async Task<SliderListDto> GetByIdAsync(int? id) => _mapper.Map<SliderListDto>(await _sliderRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _sliderRepo.DeleteAsync(await _sliderRepo.GetByIdAsync(id));

        public async Task UpdateAsync(int? id, SliderUpdateDto slider)
        {
            if (id is null) throw new ArgumentNullException();

            var existSlider = await _sliderRepo.GetByIdAsync(id) ?? throw new NullReferenceException();

            _mapper.Map(slider, existSlider);

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
