using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Services.Interface;
using DataLayer.Models;
using DataLayer.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ArtistService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void CreateArtist(ArtistCreateDto artistToCreate)
        {
            var mappedArtist = _mapper.Map<Artist>(artistToCreate);
            var anyArtist = _unitOfWork.Artists.GetAll().Any(artist => artist.Name == mappedArtist.Name);

            if (!anyArtist)
            {
                _unitOfWork.Artists.Create(mappedArtist);
                _unitOfWork.Save();
            }
        }

        public void DeleteArtist(int artistId)
        {
            _unitOfWork.Artists.Delete(artistId);
            _unitOfWork.Save();
        }

        public IEnumerable<ArtistDto> GetAllArtists()
        {
            var artists = _unitOfWork.Artists.GetAll();
            var mappedArtists = _mapper.Map<IEnumerable<ArtistDto>>(artists);
            return mappedArtists;
        }

        public ArtistDto GetArtist(int artistId)
        {
            var artistFromDB = _unitOfWork.Artists.Get(artistId);
            var mappedArtist = _mapper.Map<ArtistDto>(artistFromDB);
            return mappedArtist;
        }

        public void UpdateArtist(int artistId, ArtistUpdateDto artistToUpdate)
        {
            var artist = _unitOfWork.Artists.Get(artistId);
            artist.Name = artistToUpdate.Name;

            /*foreach (Song song in artistToUpdate.Songs)
            {
                var songToAdd = artist.Songs.FirstOrDefault(song => song.Id == song.Id);

                if (songToAdd == null)
                    artist.Songs.Add(songToAdd);
            }

            foreach (Album album in artistToUpdate.Albums)
            {
                var albumToAdd = artist.Albums.FirstOrDefault(album => album.Id == album.Id);

                if (albumToAdd == null)
                    artist.Albums.Add(albumToAdd);
            }*/

            _unitOfWork.Artists.Update(artist);
            _unitOfWork.Save();
        }
    }
}
