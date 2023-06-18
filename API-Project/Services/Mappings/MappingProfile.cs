using AutoMapper;
using Domain.Models;
using Services.DTOs.AboutInfo;
using Services.DTOs.Account;
using Services.DTOs.Advertising;
using Services.DTOs.Banner;
using Services.DTOs.Basket;
using Services.DTOs.Benefit;
using Services.DTOs.Category;
using Services.DTOs.Product;
using Services.DTOs.Slider;

namespace Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {



            CreateMap<Slider, SliderListDto>().ReverseMap();
            CreateMap<SliderCreateDto, Slider>();
            CreateMap<SliderUpdateDto, Slider>();


            CreateMap<Advertising, AdvertisingListDto>().ReverseMap();
            CreateMap<AdvertisingCreateDto, Advertising>();
            CreateMap<AdvertisingUpdateDto, Advertising>();

            CreateMap<Banner, BannerListDto>().ReverseMap();
            CreateMap<BannerCreateDto, Banner>();
            CreateMap<BannerUpdateDto, Banner>();


            CreateMap<Benefit, BenefitListDto>().ReverseMap();
            CreateMap<BenefitCreateDto, Benefit>();
            CreateMap<BenefitUpdateDto, Benefit>();


            CreateMap<AboutInfo, AboutInfoListDto>().ReverseMap();
            CreateMap<AboutInfoCreateDto, AboutInfo>();
            CreateMap<AboutInfoUpdateDto, AboutInfo>();



            CreateMap<ProductCreateDto, Product>().ReverseMap();
            CreateMap<Product, ProductListDto>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)).ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.ProductImages.Select(m => m.Image))).
                ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.ProductImages.Select(m => Convert.ToBase64String(m.Image))));


            CreateMap<ProductUpdateDto, Product>().ReverseMap();
            CreateMap<ProductGetDto, Product>().ReverseMap();

            CreateMap<BasketProductListDto, BasketProduct>().ReverseMap();



            CreateMap<Category, CategoryListDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();


            CreateMap<RegisterDto, AppUser>();
            CreateMap<AppUser, UserDto>();
        }
    }
}
