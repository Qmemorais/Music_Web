using System;
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

        public void CreateArtist(ArtistCreateDTO artistCreate)
        {
            throw new NotImplementedException();
        }

        public void DeleteArtist(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<ArtistDTO> GetAllArtistsByCountry(string country)
        {
            throw new NotImplementedException();
        }

        public ArtistDTO GetArtistById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<ArtistDTO> GetArtists()
        {
            throw new NotImplementedException();
        }

        public void UpdateArtist(ArtistUpdateDTO artistUpdate, Guid artistId)
        {
            throw new NotImplementedException();
        }
    }
}
