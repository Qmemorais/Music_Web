﻿using AutoMapper;
using BusinessLayer.Models;
using DataLayer.Models;
using Web_Music.Models;

namespace Web_Music.SourceMappingProfiles
{
    public class SourceMappingUserProfile : Profile
    {
        public SourceMappingUserProfile()
        {
            //Business-Data
            CreateMap<UserCreateDTO, User>();
            CreateMap<UserUpdateDTO, User>();
            CreateMap<UserDTOToGet, User>().ReverseMap();
            //Controllers-Business
            CreateMap<UserCreateRequestModel, UserCreateDto>().ReverseMap();
            CreateMap<UserUpdateRequestModel, UserUpdateDto>().ReverseMap();
            CreateMap<UserResponseModel, UserDto>().ReverseMap();
        }
    }
}
