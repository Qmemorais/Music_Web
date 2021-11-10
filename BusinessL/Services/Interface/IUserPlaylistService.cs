
using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interface
{
    public interface IUserPlaylistService
    {
        public void AddPlaylistToUser(UserPlaylistCreateDto addPlaylistToUser);
        public void SharePlaylistFromUser(int userId, int playlistId);
        public void DeletePlaylistFromUser(int userId, int playlistId);
        public IEnumerable<UserPlaylistDto> GetAllPlaylistsByUser(int userId);
        public UserPlaylistDto GetPlaylistByUser(int playlistId);
    }
}
