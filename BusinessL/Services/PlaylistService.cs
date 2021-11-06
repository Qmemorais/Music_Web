using AutoMapper;
using BusinessLayer.Models;
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
        private readonly IMapper _mapper;
        public PlaylistService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(PlaylistCreateDto playlistToCreate)
        {
            var playlistCreate = _mapper.Map<Playlist>(playlistToCreate);
            var GetPlaylist = _unitOfWork.Playlists.GetAll().Any(playlist => playlist.Name == playlistCreate.Name
            && playlist.UserId == playlistCreate.UserId);
            if (GetPlaylist==false)
            {
                _unitOfWork.Playlists.Create(playlistCreate);
                _unitOfWork.Save();
            }
        }

        public void Delete(int id)
        {
            _unitOfWork.Playlists.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<PlaylistUpdateDto> GetAll(int id)
        {
            var playlistsFromDB = _unitOfWork.Playlists.GetAll().Where(playlist => playlist.UserId == id);
            var playlist= _mapper.Map<IEnumerable<PlaylistUpdateDto>>(playlistsFromDB);
            return playlist;
        }

        public PlaylistUpdateDto GetPlaylist(int id)
        {
            var playlistFromDB = _unitOfWork.Playlists.Get(id);
            var playlist = _mapper.Map<PlaylistUpdateDto>(playlistFromDB);
            return playlist;
        }

        public void Update(PlaylistUpdateDto playlistToUpdate)
        {
            Playlist playlist = _mapper.Map<Playlist>(playlistToUpdate);
            _unitOfWork.Playlists.Update(playlist);
            _unitOfWork.Save();
        }

        //public bool Share(int id,int idNewUser){}
    }
}
