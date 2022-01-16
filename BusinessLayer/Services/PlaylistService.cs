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
                playlist.Time.Add(song.Time.TimeOfDay);
                _uow.Playlists.Update(playlist);
                _uow.Songs.Update(song);
                _uow.Save();
            }
        }

        public void CreatePlaylist(PlaylistCreateDTO playlistCreate)
        {
            var mappedPlaylist = _mapper.Map<Playlist>(playlistCreate);
            var anyPlaylistName = _uow.Playlists.Find(p=>p.Name==playlistCreate.Name).First();

            if (anyPlaylistName == null)
            {
                _uow.Playlists.Create(mappedPlaylist);
                _uow.Save();
            }
        }

        public List<PlaylistDTOToGet> GetAllPlaylistsBySong(Guid songId)
        {
            try
            {
                var songToGetPlaylists = _uow.Songs.Get(songId);
                var playlistsBySong = _uow.Playlists.Find(p=>p.Songs.Contains(songToGetPlaylists));
                var mappedPlaylists = _mapper.Map<IEnumerable<PlaylistDTOToGet>>(playlistsBySong).ToList();
                return mappedPlaylists;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PlaylistDTOToGet> GetAllPlaylistsByTime(DateTime time)
        {
            try
            {
                var playlistsByTime = _uow.Playlists.Find(p => p.Time == time);
                var mappedPlaylists = _mapper.Map<IEnumerable<PlaylistDTOToGet>>(playlistsByTime).ToList();
                return mappedPlaylists;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PlaylistDTOToGet> GetAllPlaylistsByUser(Guid userId)
        {
            try
            {
                var userToGetPlaylists = _uow.Users.Get(userId);
                var playlistsByUser = _uow.Playlists.Find(p => p.Users.Contains(userToGetPlaylists));
                var mappedPlaylists = _mapper.Map<IEnumerable<PlaylistDTOToGet>>(playlistsByUser).ToList();
                return mappedPlaylists;
            }
            catch (Exception)
            {
                return null; 
            }
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
            try
            {
                var playlists = _uow.Playlists.GetAll();
                var mappedPlaylists = _mapper.Map<IEnumerable<PlaylistDTOToGet>>(playlists).ToList();
                return mappedPlaylists;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void UpdatePlaylist(PlaylistUpdateDTO playlistUpdate, Guid id)
        {
            var playlist = _uow.Playlists.Get(id);

            if (playlist != null)
                if (playlist.Name != playlistUpdate.Name)
                {
                    var anyPlaylistName = _uow.Playlists.Find(p => p.Name == playlistUpdate.Name).First();

                    if (anyPlaylistName == null)
                        playlist.Name = playlistUpdate.Name;
                }
        }

        public void DeletePlaylistAsOwner(Guid userId, Guid playlistId)
        {
            var playlistToDelete = _uow.Playlists.Get(playlistId);

            if (playlistToDelete.OwnerUserId == userId)
            {
                _uow.Playlists.Delete(playlistId);
                _uow.Save();
            }
        }

        public void RemoveSongFromPlaylist(Guid playlistId, Guid songId)
        {
            var playlistToRemoveSong = _uow.Playlists.Get(playlistId);
            var songToDelete = _uow.Songs.Get(songId);
            var isSongExist = playlistToRemoveSong.Songs.Contains(songToDelete);

            if (isSongExist)
            {
                playlistToRemoveSong.Songs.Remove(songToDelete);
                _uow.Playlists.Update(playlistToRemoveSong);
                _uow.Songs.Update(songToDelete);
                _uow.Save();
            }
        }
    }
}
