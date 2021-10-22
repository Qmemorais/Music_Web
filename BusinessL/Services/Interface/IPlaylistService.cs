using DataLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interface
{
    public interface IPlaylistService
    {
        bool Create(int id, string name);
        bool Delete(int id);
        IEnumerable<Playlist> GetAll(int id);
        bool Rename(int id, string newName);
        //bool Share(int id,int idNewUser);
    }
}
