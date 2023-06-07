using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.Banner;

using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BannerService:IBannerService
    {
        private readonly IBannerRepository _bannerRepo;
        private readonly IMapper _mapper;

        public BannerService(IBannerRepository bannerRepo, IMapper mapper)
        {
            _bannerRepo = bannerRepo;
            _mapper = mapper;
        }

        public async Task CreateAsync(BannerCreateDto banner) => await _bannerRepo.CreateAsync(_mapper.Map<Banner>(banner));

        public async Task<IEnumerable<BannerListDto>> GetAllAsync() => _mapper.Map<IEnumerable<BannerListDto>>(await _bannerRepo.FindAllAsync());

        public async Task<BannerListDto> GetByIdAsync(int? id) => _mapper.Map<BannerListDto>(await _bannerRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _bannerRepo.DeleteAsync(await _bannerRepo.GetByIdAsync(id));

        public async Task UpdateAsync(int? id, BannerUpdateDto banner)
        {
            if (id is null) throw new ArgumentNullException();

            var existBanner = await _bannerRepo.GetByIdAsync(id) ?? throw new NullReferenceException();

            _mapper.Map(banner, existBanner);

            await _bannerRepo.UpdateAsync(existBanner);
        }

        public async Task<IEnumerable<BannerListDto>> SearchAsync(string? searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return _mapper.Map<IEnumerable<BannerListDto>>(await _bannerRepo.FindAllAsync());
            return _mapper.Map<IEnumerable<BannerListDto>>(await _bannerRepo.FindAllAsync(m => m.Title.Contains(searchText)));
        }

        public async Task SoftDeleteAsync(int? id)
        {
            await _bannerRepo.SoftDeleteAsync(id);
        }

    }
}
