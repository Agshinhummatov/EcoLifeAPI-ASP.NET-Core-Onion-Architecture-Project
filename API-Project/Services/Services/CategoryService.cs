using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.Category;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task CreateAsync(CategoryCreateDto category) => await _categoryRepo.CreateAsync(_mapper.Map<Category>(category));

        public async Task<IEnumerable<CategoryListDto>> GetAllAsync() => _mapper.Map<IEnumerable<CategoryListDto>>(await _categoryRepo.FindAllAsync());

        public async Task<CategoryListDto> GetByIdAsync(int? id) => _mapper.Map<CategoryListDto>(await _categoryRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _categoryRepo.DeleteAsync(await _categoryRepo.GetByIdAsync(id));

        public async Task UpdateAsync(int? id, CategoryUpdateDto category)
        {
            if (id is null) throw new ArgumentNullException();

            var existCategroy = await _categoryRepo.GetByIdAsync(id) ?? throw new NullReferenceException();

            _mapper.Map(category, existCategroy);

            await _categoryRepo.UpdateAsync(existCategroy);
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
