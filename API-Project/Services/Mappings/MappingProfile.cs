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
           


            CreateMap<Slider,SliderListDto>().ReverseMap();
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
            CreateMap<ProductListDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
            CreateMap<ProductGetDto, Product>().ReverseMap();

            CreateMap<BasketProductListDto, BasketProduct>().ReverseMap();


            CreateMap<CategoryListDto, Category>().ReverseMap();

            CreateMap<RegisterDto, AppUser>();
            CreateMap<AppUser, UserDto>();
        }
    }
}
