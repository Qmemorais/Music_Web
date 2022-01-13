using AutoMapper;
using BusinessLayer.Models;
using DataLayer.Models;
using Web_Music.Models;

namespace Web_Music.SourceMappingProfiles
{
    public class SourceMappingSongProfile:Profile
    {
        public SourceMappingSongProfile()
        {
            //Business-Data
            CreateMap<SongCreateDTO, Song>();
            CreateMap<SongUpdateDTO, Song>();
            CreateMap<SongDTOToGet, Song>().ReverseMap();
            //Controllers-Business
            CreateMap<SongCreateRequestModel, SongCreateDto>().ReverseMap();
            CreateMap<SongUpdateRequestModel, SongUpdateDto>().ReverseMap();
            CreateMap<SongResponseModel, SongDto>().ReverseMap();
        }
    }
}
