using DataLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interface
{
    public interface IPlaylistService
    {
        public void Create(Playlist PlaylistToCreate);
        public void Delete(int id);
        public IEnumerable<Playlist> GetAll(int id);
        public void Update(Playlist PlaylistToUpdate);
        public Playlist GetPlaylist(int id);
        //bool Share(int id,int idNewUser);
    }
}
