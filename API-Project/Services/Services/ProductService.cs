using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.Product;
using Services.DTOs.Slider;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ProductService :IProductService
    {
        private readonly IProductRepository _productRepo;

        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task CreateAsync(ProductCreateDto product) => await _productRepo.CreateAsync(_mapper.Map<Product>(product));

        public async Task<IEnumerable<ProductListDto>> GetAllAsync() => _mapper.Map<IEnumerable<ProductListDto>>(await _productRepo.GetAllProductsWithCategories());

        public async Task<ProductListDto> GetByIdAsync(int? id) => _mapper.Map<ProductListDto>(await _productRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _productRepo.DeleteAsync(await _productRepo.GetByIdAsync(id));

        public async Task UpdateAsync(int? id, ProductUpdateDto product)
        {
            if (id is null) throw new ArgumentNullException();

            var existProduct = await _productRepo.GetByIdAsync(id) ?? throw new NullReferenceException();

            _mapper.Map(product, existProduct);

            await _productRepo.UpdateAsync(existProduct);
        }

        public async Task<IEnumerable<ProductListDto>> SearchAsync(string? searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return _mapper.Map<IEnumerable<ProductListDto>>(await _productRepo.FindAllAsync());
            return _mapper.Map<IEnumerable<ProductListDto>>(await _productRepo.FindAllAsync(m => m.Name.Contains(searchText)));
        }

        public async Task SoftDeleteAsync(int? id)
        {
            await _productRepo.SoftDeleteAsync(id);
        }

    }
}
