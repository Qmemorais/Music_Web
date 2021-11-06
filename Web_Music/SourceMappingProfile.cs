using AutoMapper;
using BusinessLayer.Models;
using Web_Music.Models;

namespace Web_Music
{
    public class SourceMappingProfile : Profile
    {
        public SourceMappingProfile()
        {
            CreateMap<UserUpdateDto, UserResponseModel>().ReverseMap();
            CreateMap<UserCreateDto, UserCreateRequestModel>().ReverseMap();
        }
    }
}
