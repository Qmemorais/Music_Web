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
    public class PlaylistService:IPlaylistService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public PlaylistService(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public void AddSongToPlaylist(Guid playlistId, Guid songId)
        {
            var song = _uow.Songs.Get(songId);
            var playlist = _uow.Playlists.Get(playlistId);
            var isSongExistInPlaylist = playlist.Songs.Any(s => s.Id == songId);
            
            if (!isSongExistInPlaylist)
            {
                playlist.Songs.Add(song);
                song.Playlists.Add(playlist);
                _uow.Playlists.Update(playlist);
                _uow.Songs.Update(song);
                _uow.Save();
            }
        }

        public void CreatePlaylist(PlaylistCreateDTO playlistCreate)
        {
            throw new NotImplementedException();
        }

        public void DeletePlaylist(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<PlaylistDTOToGet> GetAllPlaylistsBySong(Guid songId)
        {
            throw new NotImplementedException();
        }

        public List<PlaylistDTOToGet> GetAllPlaylistsByTime(DateTime time)
        {
            throw new NotImplementedException();
        }

        public List<PlaylistDTOToGet> GetAllPlaylistsByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public PlaylistDTOToGet GetPlaylistById(Guid id)
        {
            try
            {
                var playlistFromDB = _uow.Playlists.Get(id);
                var mappedPlaylist = _mapper.Map<PlaylistDTOToGet>(playlistFromDB);
                return mappedPlaylist;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PlaylistDTOToGet> GetPlaylists()
        {
            throw new NotImplementedException();
        }

        public void UpdatePlaylist(PlaylistUpdateDTO playlistUpdate, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
