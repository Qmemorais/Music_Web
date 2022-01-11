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
    public class SongService : ISongService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public SongService(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public void CreateSong(SongCreateDTO songCreate)
        {
            var mappedSong = _mapper.Map<Song>(songCreate);
            var isSongExist = _uow.Songs.Equals(mappedSong);

            if (!isSongExist)
            {
                _uow.Songs.Create(mappedSong);
                _uow.Save();
            }
        }

        public void DeleteSong(Guid id)
        {
            var song = _uow.Songs.Get(id);

            if (song != null)
            {
                _uow.Songs.Delete(id);
                _uow.Save();
            }
        }

        public List<SongDTOToGet> GetAllSongsByAlbum(Guid AlbumId)
        {
            throw new NotImplementedException();
        }

        public List<SongDTOToGet> GetAllSongsByArtist(Guid artistId)
        {
            throw new NotImplementedException();
        }

        public List<SongDTOToGet> GetAllSongsByPlaylist(Guid playlistId)
        {
            throw new NotImplementedException();
        }

        public List<SongDTOToGet> GetAllSongsByTime(DateTime time)
        {
            throw new NotImplementedException();
        }

        public SongDTOToGet GetSongById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<SongDTOToGet> GetSongs()
        {
            throw new NotImplementedException();
        }

        public void UpdateSong(SongUpdateDTO songUpdate, Guid id)
        {
            var song = _uow.Songs.Get(id);

            if (song != null)
            {
                song.Genre = songUpdate.Genre;
                song.Time = songUpdate.Time;
                song.Title = songUpdate.Title;
                _uow.Songs.Update(song);
                _uow.Save();
            }
        }
    }
}
