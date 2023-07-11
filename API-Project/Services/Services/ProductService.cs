using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.Advertising;
using Services.DTOs.Product;
using Services.DTOs.Slider;
using Services.Helpers;
using Services.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
      

        public ProductService(IProductRepository productRepo, IMapper mapper, IBasketRepository basketRepository)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _basketRepository = basketRepository;
        }

        public async Task CreateAsync(ProductCreateDto producCreatetDto)
        {

            var validationContext = new ValidationContext(producCreatetDto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(producCreatetDto, validationContext, validationResults, true);

            if (!isValid)
            {
                string errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new Exception(errorMessages);
            }

            if (string.IsNullOrEmpty(producCreatetDto.Name) || string.IsNullOrEmpty(producCreatetDto.Description))
            {
                throw new Exception("Title and Description are required.");
            }

            var mapAdvertising = _mapper.Map<Product>(producCreatetDto);
            mapAdvertising.Image = await producCreatetDto.Photo.GetBytes();
            mapAdvertising.HoverImage = await producCreatetDto.HoverPhoto.GetBytes();
            await _productRepo.CreateAsync(mapAdvertising);

        }

        public async Task<IEnumerable<ProductListDto>> GetAllAsync() => _mapper.Map<IEnumerable<ProductListDto>>(await _productRepo.GetAllProductsWithCategories());

        public async Task<ProductListDto> GetByIdAsync(int? id) => _mapper.Map<ProductListDto>(await _productRepo.GetByIdAsync(id));



        public async Task DeleteAsync(int? id) => await _productRepo.DeleteAsync(await _productRepo.GetByIdAsync(id));


        public async Task UpdateAsync(int? id, ProductUpdateDto productUpdateDto)
        {
            if (id is null) throw new ArgumentNullException();

            var existingProduct = await _productRepo.GetByIdAsync(id) ?? throw new NullReferenceException();

            existingProduct.Name = productUpdateDto.Name ?? existingProduct.Name;
            existingProduct.Description = productUpdateDto.Description ?? existingProduct.Description;
            existingProduct.Rates = productUpdateDto.Rates ?? existingProduct.Rates;
            existingProduct.Price = productUpdateDto.Price ?? existingProduct.Price;
            existingProduct.Count = productUpdateDto.Count ?? existingProduct.Count;


            if (productUpdateDto.Photo != null)
            {
                existingProduct.Image = await productUpdateDto.Photo.GetBytes();
            }

            if (productUpdateDto.HoverPhoto != null)
            {
                existingProduct.HoverImage = await productUpdateDto.HoverPhoto.GetBytes();
            }

            if (existingProduct.CategoryId != productUpdateDto.CategoryId)
            {
                existingProduct.CategoryId = productUpdateDto.CategoryId;
                existingProduct.Category = null; // Resetting the Category reference to ensure it's reloaded when needed
            }

            await _productRepo.UpdateAsync(existingProduct);
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
        

       
        public async Task<List<Product>> GetProductCategory(int categorId)
        {
           return await _productRepo.GetCategoryProduct(categorId);
        }

        public async Task<int> GetCategoryProductCount(int categoryId)
        {
            return await _productRepo.GetCategoryProductCount(categoryId);
        }
    }
}
