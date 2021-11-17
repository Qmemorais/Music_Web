using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Interface;
using DataLayer.Context;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Services
{
    public class ArtistService : IArtistService
    {
        private readonly MusicContext _db;
        private readonly IMapper _mapper;


        public ArtistService(MusicContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public void CreateArtist(ArtistCreateDto artistToCreate)
        {
            var mappedArtist = _mapper.Map<Artist>(artistToCreate);
            var anyArtist = _db.Artists.Any(artist => artist.Name == mappedArtist.Name);

            if (!anyArtist)
            {
                _db.Artists.Add(mappedArtist);
                _db.SaveChanges();
            }
        }

        public void DeleteArtist(int artistId)
        {
            var artist = _db.Artists.Find(artistId);
            _db.Artists.Remove(artist);
            _db.SaveChanges();
        }

        public IEnumerable<ArtistDto> GetAllArtists()
        {
            var artists = _db.Artists.Include(x => x.Albums).Include(s => s.Songs);
            var mappedArtists = _mapper.Map<IEnumerable<ArtistDto>>(artists);
            return mappedArtists;
        }

        public ArtistDto GetArtist(int artistId)
        {
            var artistFromDB = _db.Artists.Include(x => x.Albums).Include(s => s.Songs).Where(artist => artist.Id == artistId).First();
            var mappedArtist = _mapper.Map<ArtistDto>(artistFromDB);
            return mappedArtist;
        }

        public void UpdateArtist(int artistId, ArtistUpdateDto artistToUpdate)
        {
            var artist = _db.Artists.Find(artistId);
            artist.Name = artistToUpdate.Name;
            _db.Artists.Update(artist);
            _db.SaveChanges();
        }
    }
}
