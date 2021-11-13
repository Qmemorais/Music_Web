using AutoMapper;
using BusinessLayer.Models;
using DataLayer.Models;
using Web_Music.Models;

namespace Web_Music.SourceMappingProfiles
{
    public class SourceMappingAlbumProfile : Profile
    {
        public SourceMappingAlbumProfile()
        {
            //Business-Data
            CreateMap<AlbumCreateDto, Album>().ReverseMap();
            CreateMap<AlbumUpdateDto, Album>().ReverseMap();
            CreateMap<AlbumDto, Album>().ReverseMap();
            //Controllers-Business
            CreateMap<AlbumCreateRequestModel, AlbumCreateDto>().ReverseMap();
            CreateMap<AlbumUpdateRequestModel, AlbumUpdateDto>().ReverseMap();
            CreateMap<AlbumResponseModel, AlbumDto>().ReverseMap();
        }
    }
}
