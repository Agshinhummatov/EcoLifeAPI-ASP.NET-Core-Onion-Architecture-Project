using AutoMapper;
using Domain.Models;
using Services.DTOs.Account;

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

            CreateMap<RegisterDto, AppUser>();
            CreateMap<AppUser, UserDto>();
        }
    }
}
