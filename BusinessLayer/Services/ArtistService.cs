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
            var mappedArtist = _mapper.Map<Artist>(artistCreate);
            var anyArtistWithName = _uow.Artists.Find(a => a.Name == artistCreate.Name).First();

            if(anyArtistWithName == null)
            {
                _uow.Artists.Create(mappedArtist);
                _uow.Save();
            }
        }

        public void DeleteArtist(Guid id)
        {
            var artist = _uow.Artists.Get(id);

            if (artist != null)
            {
                _uow.Artists.Delete(id);
                _uow.Save();
            }
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
            var artist = _uow.Artists.Get(artistId);

            if (artist != null)
            {
                artist.Country = artistUpdate.Country;
                if (artist.Name != artistUpdate.Name)
                {
                    var anyArtistWithNewName = _uow.Artists.Find(a => a.Name == artistUpdate.Name).First();

                    if (anyArtistWithNewName == null)
                        artist.Name = artistUpdate.Name;
                }
                _uow.Artists.Update(artist);
                _uow.Save();
            }
        }
    }
}
