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
        public PlaylistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(PlaylistCreateDto playlistToCreate)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PlaylistCreateDto, Playlist>());
            var mapper = new Mapper(config);
            Playlist PlaylistCreate = mapper.Map<PlaylistCreateDto, Playlist>(playlistToCreate);
            var GetPlaylist = _unitOfWork.Playlists.GetAll().Any(playlist => playlist.Name == PlaylistCreate.Name
            && playlist.UserId == PlaylistCreate.UserId);
            if (GetPlaylist==false)
            {
                _unitOfWork.Playlists.Create(PlaylistCreate);
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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Playlist, PlaylistUpdateDto>());
            var mapper = new Mapper(config);
            var playlistsFromDB = _unitOfWork.Playlists.GetAll().Where(playlist => playlist.UserId == id);
            var playlist= mapper.Map<List<PlaylistUpdateDto>>(playlistsFromDB);
            return playlist;
        }

        public PlaylistUpdateDto GetPlaylist(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Playlist, PlaylistUpdateDto>());
            var mapper = new Mapper(config);
            var playlistFromDB = _unitOfWork.Playlists.Get(id);
            var playlist = mapper.Map<PlaylistUpdateDto>(playlistFromDB);
            return playlist;
        }

        public void Update(PlaylistUpdateDto playlistToUpdate)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PlaylistUpdateDto, Playlist>());
            var mapper = new Mapper(config);
            Playlist playlist = mapper.Map<PlaylistUpdateDto, Playlist>(playlistToUpdate);
            _unitOfWork.Playlists.Update(playlist);
            _unitOfWork.Save();
        }

        //public bool Share(int id,int idNewUser){}
    }
}
