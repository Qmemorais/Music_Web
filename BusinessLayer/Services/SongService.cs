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
            var songsByAlbum = _uow.Songs.Find(s => s.AlbumId == AlbumId);
            var mappedSongs = _mapper.Map<IEnumerable<SongDTOToGet>>(songsByAlbum).ToList();
            return mappedSongs;
        }

        public List<SongDTOToGet> GetAllSongsByArtist(Guid artistId)
        {
            var songsByArtist = _uow.Songs.Find(s => s.ArtistId == artistId);
            var mappedSongs = _mapper.Map<IEnumerable<SongDTOToGet>>(songsByArtist).ToList();
            return mappedSongs;
        }

        public List<SongDTOToGet> GetAllSongsByPlaylist(Guid playlistId)
        {
            var playlistToGetSongs = _uow.Playlists.Get(playlistId);
            var songsByPlaylist = playlistToGetSongs.Songs;
            var mappedSongs = _mapper.Map<IEnumerable<SongDTOToGet>>(songsByPlaylist).ToList();
            return mappedSongs;
        }

        public List<SongDTOToGet> GetAllSongsByTime(DateTime time)
        {
            var songsByTime = _uow.Songs.Find(s => s.Time == time);
            var mappedSongs = _mapper.Map<IEnumerable<SongDTOToGet>>(songsByTime).ToList();
            return mappedSongs;
        }

        public SongDTOToGet GetSongById(Guid id)
        {
            try
            {
                var songFromDB = _uow.Songs.Get(id);
                var mappedSong = _mapper.Map<SongDTOToGet>(songFromDB);
                return mappedSong;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SongDTOToGet> GetSongs()
        {
            try
            {
                var songsFromDB = _uow.Songs.GetAll();
                var mappedSongs = _mapper.Map<IEnumerable<SongDTOToGet>>(songsFromDB).ToList();
                return mappedSongs;
            }
            catch (Exception)
            {
                return null;
            }
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
