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
            throw new NotImplementedException();
        }

        public void DeleteSong(Guid id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
