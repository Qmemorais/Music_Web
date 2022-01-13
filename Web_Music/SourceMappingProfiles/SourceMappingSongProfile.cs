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
            CreateMap<SongDTO, Song>().ReverseMap();
            //Controllers-Business
            CreateMap<SongCreateRequestModel, SongCreateDTO>();
            CreateMap<SongUpdateRequestModel, SongUpdateDTO>();
            CreateMap<SongResponseModel, SongDTOToGet>().ReverseMap();
            CreateMap<SongModel, SongDTO>().ReverseMap();
        }
    }
}
