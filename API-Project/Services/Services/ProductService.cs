using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.Product;
using Services.Helpers;
using Services.Services.Interfaces;

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

        public async Task CreateAsync(ProductCreateDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                CategoryId = productDto.CategoryId,
                Rates = productDto.Rates,
                Count = productDto.Count,
                

            };

            var productImages = new List<ProductImage>();

            foreach (var imageDto in productDto.Images)
            {
                var productImage = new ProductImage
                {
                    Image = await imageDto.GetBytes(),
                    Product = product

                };

                productImages.Add(productImage);
            }

            product.ProductImages = productImages;

            await _productRepo.CreateAsync(product);
        }

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
