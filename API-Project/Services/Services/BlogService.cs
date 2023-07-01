using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.Advertising;
using Services.DTOs.Blog;
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
    public class BlogService:IBlogService
    {
        private readonly IBlogRepository _blogRepo;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository blogRepo, IMapper mapper)
        {
            _blogRepo= blogRepo;
            _mapper= mapper;
        }


        public async Task CreateAsync(BlogCreateDto blogCreateDto)
        {

            var validationContext = new ValidationContext(blogCreateDto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(blogCreateDto, validationContext, validationResults, true);

            if (!isValid)
            {
                string errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new Exception(errorMessages);
            }

            if (string.IsNullOrEmpty(blogCreateDto.Title) || string.IsNullOrEmpty(blogCreateDto.Description))
            {
                throw new Exception("Title and Description are required.");
            }

            var mapAdvertising = _mapper.Map<Blog>(blogCreateDto);
            mapAdvertising.Image = await blogCreateDto.Photo.GetBytes();
            await _blogRepo.CreateAsync(mapAdvertising);


        }



        public async Task<IEnumerable<BlogListDto>> GetAllAsync() => _mapper.Map<IEnumerable<BlogListDto>>(await _blogRepo.FindAllAsync());

        public async Task<BlogListDto> GetByIdAsync(int? id) => _mapper.Map<BlogListDto>(await _blogRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _blogRepo.DeleteAsync(await _blogRepo.GetByIdAsync(id));


        public async Task UpdateAsync(int? id, BlogUpdateDto blogUpdateDto)
        {

            if (id is null) throw new ArgumentNullException();

            var existBlog = await _blogRepo.GetByIdAsync(id) ?? throw new NullReferenceException();

            existBlog.Title = blogUpdateDto.Title ?? existBlog.Title;
            existBlog.Description = blogUpdateDto.Description ?? existBlog.Description;

            if (blogUpdateDto.Photo != null)
            {

                existBlog.Image = await blogUpdateDto.Photo.GetBytes();
            }


            await _blogRepo.UpdateAsync(existBlog);
        }



        public async Task<IEnumerable<BlogListDto>> SearchAsync(string? searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return _mapper.Map<IEnumerable<BlogListDto>>(await _blogRepo.FindAllAsync());
            return _mapper.Map<IEnumerable<BlogListDto>>(await _blogRepo.FindAllAsync(m => m.Title.Contains(searchText)));
        }

        public async Task SoftDeleteAsync(int? id)
        {
            await _blogRepo.SoftDeleteAsync(id);
        }


    }
}
