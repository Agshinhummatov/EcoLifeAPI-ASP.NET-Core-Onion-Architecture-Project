using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.Benefit;
using Services.DTOs.Category;
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
    public class CategoryService :ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var validationContext = new ValidationContext(categoryCreateDto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(categoryCreateDto, validationContext, validationResults, true);

            if (!isValid)
            {
                string errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new Exception(errorMessages);
            }

            if (string.IsNullOrEmpty(categoryCreateDto.Name))
            {
                throw new Exception("Name is required.");
            }

            var mapCategory = _mapper.Map<Category>(categoryCreateDto);
            mapCategory.CategoryImage = await categoryCreateDto.Photo.GetBytes();
            await _categoryRepo.CreateAsync(mapCategory);

        }

        public async Task<IEnumerable<CategoryListDto>> GetAllAsync() => _mapper.Map<IEnumerable<CategoryListDto>>(await _categoryRepo.FindAllAsync());

        public async Task<CategoryListDto> GetByIdAsync(int? id) => _mapper.Map<CategoryListDto>(await _categoryRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _categoryRepo.DeleteAsync(await _categoryRepo.GetByIdAsync(id));

        public async Task UpdateAsync(int? id, CategoryUpdateDto categoryUpdateDto)
        {

            if (id is null) throw new ArgumentNullException();

            var existCategory = await _categoryRepo.GetByIdAsync(id) ?? throw new NullReferenceException();

            existCategory.Name = categoryUpdateDto.Name ?? existCategory.Name;


            if (categoryUpdateDto.Photo != null)
            {

                existCategory.CategoryImage = await categoryUpdateDto.Photo.GetBytes();
            }

            await _categoryRepo.UpdateAsync(existCategory);
        }

        public async Task<IEnumerable<CategoryListDto>> SearchAsync(string? searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return _mapper.Map<IEnumerable<CategoryListDto>>(await _categoryRepo.FindAllAsync());
            return _mapper.Map<IEnumerable<CategoryListDto>>(await _categoryRepo.FindAllAsync(m => m.Name.Contains(searchText)));
        }

        public async Task SoftDeleteAsync(int? id)
        {
            await _categoryRepo.SoftDeleteAsync(id);
        }
    }
}
