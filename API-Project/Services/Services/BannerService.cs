using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Services.DTOs.Banner;
using Services.Helpers;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

      


        public async Task CreateAsync(BannerCreateDto bannerCreateDto)
        {
            var validationContext = new ValidationContext(bannerCreateDto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(bannerCreateDto, validationContext, validationResults, true);

            if (!isValid)
            {
                string errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new Exception(errorMessages);
            }

            if (string.IsNullOrEmpty(bannerCreateDto.Title) || string.IsNullOrEmpty(bannerCreateDto.Description))
            {
                throw new Exception("Title and Description are required.");
            }

           

            var mapBanner = _mapper.Map<Banner>(bannerCreateDto);
            mapBanner.Image = await bannerCreateDto.Photo.GetBytes();
            await _bannerRepo.CreateAsync(mapBanner);

        }


        public async Task<IEnumerable<BannerListDto>> GetAllAsync() => _mapper.Map<IEnumerable<BannerListDto>>(await _bannerRepo.FindAllAsync());

        public async Task<BannerListDto> GetByIdAsync(int? id) => _mapper.Map<BannerListDto>(await _bannerRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _bannerRepo.DeleteAsync(await _bannerRepo.GetByIdAsync(id));

        public async Task UpdateAsync(int? id, BannerUpdateDto bannerUpdateDto)
        {
          

            if (id is null) throw new ArgumentNullException();

            var existBanner = await _bannerRepo.GetByIdAsync(id) ?? throw new NullReferenceException();

            existBanner.Title = bannerUpdateDto.Title ?? existBanner.Title;
            existBanner.Description = bannerUpdateDto.Description ?? existBanner.Description;

            if (bannerUpdateDto.Photo != null)
            {
                
                existBanner.Image = await bannerUpdateDto.Photo.GetBytes();
            }

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
