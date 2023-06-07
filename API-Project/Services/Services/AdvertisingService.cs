using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.Advertising;
using Services.DTOs.Slider;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
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


        public async Task CreateAsync(AdvertisingCreateDto advertising) => await _adverstingRepo.CreateAsync(_mapper.Map<Advertising>(advertising));

        public async Task<IEnumerable<AdvertisingListDto>> GetAllAsync() => _mapper.Map<IEnumerable<AdvertisingListDto>>(await _adverstingRepo.FindAllAsync());

        public async Task<AdvertisingListDto> GetByIdAsync(int? id) => _mapper.Map<AdvertisingListDto>(await _adverstingRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _adverstingRepo.DeleteAsync(await _adverstingRepo.GetByIdAsync(id));

        public async Task UpdateAsync(int? id, AdvertisingUpdateDto advertising)
        {
            if (id is null) throw new ArgumentNullException();

            var existAdvertising = await _adverstingRepo.GetByIdAsync(id) ?? throw new NullReferenceException();

            _mapper.Map(advertising, existAdvertising);

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
