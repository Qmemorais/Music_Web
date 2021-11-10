using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interface
{
    public interface IPlaylistSongService
    {
        public void AddSongToPlaylist(PlaylistSongCreateDto addSongToPlaylist);
        public void DeleteSongFromPlaylist(int playlistId, int songId);
        public IEnumerable<PlaylistSongDto> GetAllSongsByPlaylist(int playlistId);
        public PlaylistSongDto GetSongByPlaylist(int songId);
    }
}
