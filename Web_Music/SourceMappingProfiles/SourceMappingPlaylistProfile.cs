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
            CreateMap<PlaylistCreateDTO, Playlist>();
            CreateMap<PlaylistUpdateDTO, Playlist>();
            CreateMap<PlaylistDTOToGet, Playlist>().ReverseMap();
            CreateMap<PlaylistDTO, Playlist>().ReverseMap();
            //Controllers-Business
            CreateMap<PlaylistCreateRequestModel, PlaylistCreateDTO>();
            CreateMap<PlaylistUpdateRequestModel, PlaylistUpdateDTO>();
            CreateMap<PlaylistResponseModel, PlaylistDTOToGet>().ReverseMap();
            CreateMap<PlaylistModel, PlaylistDTO>().ReverseMap();
        }
    }
}
