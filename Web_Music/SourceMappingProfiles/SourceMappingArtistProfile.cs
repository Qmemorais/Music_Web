using AutoMapper;
using BusinessLayer.Models;
using DataLayer.Models;
using Web_Music.Models;

namespace Web_Music.SourceMappingProfiles
{
    public class SourceMappingArtistProfile : Profile
    {
        public SourceMappingArtistProfile()
        {
            //Business-Data
            CreateMap<ArtistCreateDto, Artist>().ReverseMap();
            CreateMap<ArtistUpdateDto, Artist>().ReverseMap();
            CreateMap<ArtistDto, Artist>().ReverseMap();
            //Controllers-Business
            CreateMap<ArtistCreateRequestModel, ArtistCreateDto>().ReverseMap();
            CreateMap<ArtistUpdateRequestModel, ArtistUpdateDto>().ReverseMap();
            CreateMap<ArtistResponseModel, ArtistDto>().ReverseMap();
        }
    }
}
