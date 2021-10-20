using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository.Interface;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class PlaylistSerice
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlaylistSerice(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool CreateNewPlaylist(int id, string name)
        {
            _unitOfWork.Generic.Create(new Playlist { Name = name, UserId = id });
            _unitOfWork.Save();
            return true;
        }
        public bool RenamePlaylist(int id, string newName)
        {
            Playlist playlist = (Playlist)_unitOfWork.Generic.Get(id);
            playlist.Name = newName ?? playlist.Name;
            _unitOfWork.Generic.Update(playlist);
            _unitOfWork.Save();
            return true;

        }
        public bool DeletePlaylist(int id)
        {
            _unitOfWork.Generic.Delete(id);
            _unitOfWork.Save();
            return true;
        }
        public IEnumerable<Playlist> PrintAllPlaylists(int id)
        {
            return _unitOfWork.GetList.GetAll(id);
            //return playlistsUser;
        }
    }
}
