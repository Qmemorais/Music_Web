using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interface
{
    public interface IPlaylistService
    {
        public void Create(PlaylistCreateDto playlistToCreate);
        public void Delete(int id);
        public IEnumerable<PlaylistUpdateDto> GetAll(int id);
        public void Update(PlaylistUpdateDto playlistToUpdate);
        public PlaylistUpdateDto GetPlaylist(int id);
        //bool Share(int id,int idNewUser);
    }
}
