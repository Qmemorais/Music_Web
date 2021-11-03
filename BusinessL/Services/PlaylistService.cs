using BusinessLayer.Services.Interface;
using DataLayer.Models;
using DataLayer.UnitOfWork;
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

        public void Create(Playlist PlaylistToCreate)
        {
            var GetPlaylist = _unitOfWork.Playlists.GetAll().Any(playlist => playlist.Name == PlaylistToCreate.Name
            && playlist.UserId == PlaylistToCreate.UserId);
            if (GetPlaylist==false)
            {
                _unitOfWork.Playlists.Create(PlaylistToCreate);
                _unitOfWork.Save();
            }
        }

        public void Delete(int id)
        {
            _unitOfWork.Playlists.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<Playlist> GetAll(int id)
        {
            return _unitOfWork.Playlists.GetAll().Where(playlist => playlist.UserId == id);
        }

        public Playlist GetPlaylist(int id)
        {
            return _unitOfWork.Playlists.Get(id);
        }

        public void Update(Playlist PlaylistToUpdate)
        {
            _unitOfWork.Playlists.Update(PlaylistToUpdate);
            _unitOfWork.Save();
        }

        //public bool Share(int id,int idNewUser){}
    }
}
