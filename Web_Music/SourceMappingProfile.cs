using AutoMapper;
using BusinessLayer.Models;
using DataLayer.Models;
using Web_Music.Models;

namespace BusinessLayer
{
    public class SourceMappingProfile : Profile
    {
        public SourceMappingProfile()
        {
            CreateMap<UserCreateDto, User>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<UserResponseModel, UserUpdateDto>().ReverseMap();
            CreateMap<UserCreateRequestModel, UserCreateDto>().ReverseMap();
            CreateMap<PlaylistCreateDto, Playlist>().ReverseMap();
            CreateMap<PlaylistUpdateDto, Playlist>().ReverseMap();
            CreateMap<SongCreateDto, Song>().ReverseMap();
            CreateMap<SongUpdateDto, Song>().ReverseMap();
        }
    }
}
