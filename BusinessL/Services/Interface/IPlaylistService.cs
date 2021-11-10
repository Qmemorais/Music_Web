using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interface
{
    public interface IPlaylistService
    {
        public void CreatePlaylist(PlaylistCreateDto playlistToCreate);
        public void DeletePlaylist(int playlistId);
        public void UpdatePlaylist(int playlistId, PlaylistUpdateDto playlistToUpdate);
        public IEnumerable<PlaylistDto> GetAllPlaylists();
        public PlaylistDto GetPlaylist(int playlistId);
    }
}
