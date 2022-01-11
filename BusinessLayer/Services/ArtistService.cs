﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Interfaces;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace BusinessLayer.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public ArtistService(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }
    }
}