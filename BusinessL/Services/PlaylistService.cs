using BusinessLayer.Services.Interface;
using DataLayer.Models;
using DataLayer.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Services
{
    public class PlaylistService: IPlaylistService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlaylistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(int id, string name)
        {
            if (_unitOfWork.Playlist.GetAll().Where(playlist => playlist.Name == name) == null)
            {
                _unitOfWork.Playlist.Create(new Playlist { Name = name, UserId=id });
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            _unitOfWork.Playlist.Delete(_unitOfWork.Playlist.Get(id));
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Playlist> GetAll(int id)
        {
            return _unitOfWork.Playlist.GetAll().Where(playlist => playlist.UserId == id);
        }

        public bool Rename(int id, string newName)
        {
            Playlist playlist = _unitOfWork.Playlist.Get(id);
            playlist.Name = newName;
            _unitOfWork.Playlist.Update(playlist);
            _unitOfWork.Save();
            return true;
        }

        //public bool Share(int id,int idNewUser){}
    }
}
