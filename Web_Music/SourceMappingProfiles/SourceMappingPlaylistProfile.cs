using AutoMapper;
using BusinessLayer.Models;
using DataLayer.Models;
using Web_Music.Models;

namespace Web_Music.SourceMappingProfiles
{
    public class SourceMappingPlaylistProfile : Profile
    {
        public SourceMappingPlaylistProfile()
        {
            //Business-Data
            CreateMap<PlaylistCreateDto, Playlist>().ReverseMap();
            CreateMap<PlaylistUpdateDto, Playlist>().ReverseMap();
            CreateMap<PlaylistDto, Playlist>().ReverseMap();
            //Controllers-Business
            CreateMap<PlaylistCreateRequestModel, PlaylistCreateDto>().ReverseMap();
            CreateMap<PlaylistUpdateRequestModel, PlaylistUpdateDto>().ReverseMap();
            CreateMap<PlaylistResponseModel, PlaylistDto>().ReverseMap();
        }
    }
}
