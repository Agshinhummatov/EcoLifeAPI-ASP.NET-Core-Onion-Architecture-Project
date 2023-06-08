using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.AboutInfo;
using Services.DTOs.Slider;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task CreateAsync(AboutInfoCreateDto about) => await _aboutRepo.CreateAsync(_mapper.Map<AboutInfo>(about));

        public async Task<IEnumerable<AboutInfoListDto>> GetAllAsync() => _mapper.Map<IEnumerable<AboutInfoListDto>>(await _aboutRepo.FindAllAsync());

        public async Task<AboutInfoListDto> GetByIdAsync(int? id) => _mapper.Map<AboutInfoListDto>(await _aboutRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _aboutRepo.DeleteAsync(await _aboutRepo.GetByIdAsync(id));

        public async Task UpdateAsync(int? id, AboutInfoUpdateDto about)
        {
            if (id is null) throw new ArgumentNullException();

            var existAbout = await _aboutRepo.GetByIdAsync(id) ?? throw new NullReferenceException();

            _mapper.Map(about, existAbout);

            await _aboutRepo.UpdateAsync(existAbout);
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
