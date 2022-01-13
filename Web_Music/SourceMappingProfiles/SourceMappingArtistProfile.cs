﻿using AutoMapper;
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
            CreateMap<ArtistCreateDTO, Artist>();
            CreateMap<ArtistUpdateDTO, Artist>();
            CreateMap<ArtistDTO, Artist>().ReverseMap();
            //Controllers-Business
            CreateMap<ArtistCreateRequestModel, ArtistCreateDto>().ReverseMap();
            CreateMap<ArtistUpdateRequestModel, ArtistUpdateDto>().ReverseMap();
            CreateMap<ArtistResponseModel, ArtistDto>().ReverseMap();
        }
    }
}
