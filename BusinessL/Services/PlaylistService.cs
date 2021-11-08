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

        public void CreatePlaylist(PlaylistCreateDto playlistToCreate)
        {
            var playlistCreate = _mapper.Map<Playlist>(playlistToCreate);
            var isPlaylistExisting = _unitOfWork.Playlists.GetAll().Any(playlist => playlist.Name == playlistCreate.Name
            && playlist.UserId == playlistCreate.UserId);
            if (!isPlaylistExisting)
            {
                _unitOfWork.Playlists.Create(playlistCreate);
                _unitOfWork.Save();
            }
        }

        public void DeletePlaylist(int playlistId)
        {
            _unitOfWork.Playlists.Delete(playlistId);
            _unitOfWork.Save();
        }

        public IEnumerable<PlaylistDto> GetAllPlaylistsByUser(int userId)
        {
            var allPlaylistsByUser = _unitOfWork.Playlists.GetAll().Where(playlist => playlist.UserId == userId);
            var playlist= _mapper.Map<IEnumerable<PlaylistDto>>(allPlaylistsByUser);
            return playlist;
        }

        public PlaylistDto GetPlaylist(int playlistId)
        {
            var playlistFromDB = _unitOfWork.Playlists.Get(playlistId);
            var playlist = _mapper.Map<PlaylistDto>(playlistFromDB);
            return playlist;
        }

        public void UpdatePlaylist(int playlistId, PlaylistUpdateDto playlistToUpdate)
        {
            var playlist = _unitOfWork.Playlists.Get(playlistId);
            playlist.Name = playlistToUpdate.Name;
            _unitOfWork.Playlists.Update(playlist);
            _unitOfWork.Save();
        }

        //public bool Share(int id,int idNewUser){}
    }
}
