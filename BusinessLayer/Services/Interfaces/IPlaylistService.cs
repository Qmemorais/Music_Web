using System;
using System.Collections.Generic;
using BusinessLayer.Models;

namespace BusinessLayer.Services.Interfaces
{
    public interface IPlaylistService
    {
        public void CreatePlaylist(PlaylistCreateDTO playlistCreate);
        public void UpdatePlaylist(PlaylistUpdateDTO playlistUpdate, Guid id);
        public void DeletePlaylistAsOwner(Guid userId, Guid playlistId);
        public List<PlaylistDTOToGet> GetPlaylists();
        public PlaylistDTOToGet GetPlaylistById(Guid id);
        public void AddSongToPlaylist(Guid playlistId, Guid songId);
        public List<PlaylistDTOToGet> GetAllPlaylistsByUser(Guid userId);
        public List<PlaylistDTOToGet> GetAllPlaylistsBySong(Guid songId);
        //take all playlists where time is more then we asked
        public List<PlaylistDTOToGet> GetAllPlaylistsByTime(DateTime time);
    }
}
