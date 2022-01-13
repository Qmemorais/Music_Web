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
            //Controllers-Business
            CreateMap<PlaylistCreateRequestModel, PlaylistCreateDto>().ReverseMap();
            CreateMap<PlaylistUpdateRequestModel, PlaylistUpdateDto>().ReverseMap();
            CreateMap<PlaylistResponseModel, PlaylistDto>().ReverseMap();
        }
    }
}
