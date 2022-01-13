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
            CreateMap<AlbumCreateDTO, Album>();
            CreateMap<AlbumUpdateDTO, Album>();
            CreateMap<AlbumDTOToGet, Album>().ReverseMap();
            //Controllers-Business
            CreateMap<AlbumCreateRequestModel, AlbumCreateDTO>();
            CreateMap<AlbumUpdateRequestModel, AlbumUpdateDTO>();
            CreateMap<AlbumResponseModel, AlbumDTOToGet>().ReverseMap();
        }
    }
}
