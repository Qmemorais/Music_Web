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
            try
            {
                var artistsByCountry = _uow.Artists.Find(a => a.Country == country);
                var mappedArtists = _mapper.Map<IEnumerable<ArtistDTO>>(artistsByCountry).ToList();
                return mappedArtists;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ArtistDTO GetArtistById(Guid id)
        {
            try
            {
                var artistFromContext = _uow.Artists.Get(id);
                var mappedArtist = _mapper.Map<ArtistDTO>(artistFromContext);
                return mappedArtist;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ArtistDTO> GetArtists()
        {
            try
            {
                var artistsFromContext = _uow.Artists.GetAll();
                var mappedArtists = _mapper.Map<IEnumerable<ArtistDTO>>(artistsFromContext).ToList();
                return mappedArtists;
            }
            catch (Exception)
            {
                return null;
            }
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
