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

        public void AddSongToPlaylist(int playlistId, int songId)
        {
            var playlist = _unitOfWork.Playlists.Get(playlistId);
            var isSongExist = playlist.Songs.Any(song => song.Id == songId);

            if (!isSongExist)
            {
                var song = _unitOfWork.Songs.Get(songId);
                playlist.Songs.Add(song);
                song.Playlists.Add(playlist);
                _unitOfWork.Playlists.Update(playlist);
                _unitOfWork.Songs.Update(song);
                _unitOfWork.Save();
            }
        }

        public void CreatePlaylist(PlaylistCreateDto playlistToCreate)
        {
            var userWhoCreatePlaylist = _unitOfWork.Users.Get(playlistToCreate.UserId);
            var playlistCreate = _mapper.Map<Playlist>(playlistToCreate);
            var isPlaylistExisting = _unitOfWork.Playlists.GetAll().Any(playlist => playlist.Name == playlistCreate.Name);

            if (!isPlaylistExisting)
            {
                var newPlaylist = _unitOfWork.Playlists.Create(playlistCreate);
                userWhoCreatePlaylist.Playlists.Add(newPlaylist);
                newPlaylist.Users.Add(userWhoCreatePlaylist);

                _unitOfWork.Save();
            }
        }

        public void DeletePlaylist(int playlistId)
        {
            _unitOfWork.Playlists.Delete(playlistId);
            _unitOfWork.Save();
        }

        public IEnumerable<PlaylistDto> GetAllPlaylists()
        {
            var allPlaylists = _unitOfWork.Playlists.GetAll();
            var mappedPlaylists = _mapper.Map<IEnumerable<PlaylistDto>>(allPlaylists);
            return mappedPlaylists;
        }

        public IEnumerable<PlaylistDto> GetAllPlaylistsBySong(int songId)
        {
            var songToGetPlaylists = _unitOfWork.Songs.Get(songId);
            var allPlaylistsBySong = _unitOfWork.Playlists.GetAll().Any(playlist => playlist.Songs.Contains(songToGetPlaylists));
            var mappedPlaylists = _mapper.Map<IEnumerable<PlaylistDto>>(allPlaylistsBySong);
            return mappedPlaylists;
        }

        public IEnumerable<PlaylistDto> GetAllPlaylistsByUser(int userId)
        {
            var userToGetPlaylists = _unitOfWork.Users.Get(userId);
            var allPlaylistsByUser = _unitOfWork.Playlists.GetAll().Any(playlist => playlist.Users.Contains(userToGetPlaylists));
            var mappedPlaylists= _mapper.Map<IEnumerable<PlaylistDto>>(allPlaylistsByUser);
            return mappedPlaylists;
        }

        public PlaylistDto GetPlaylist(int playlistId)
        {
            var playlistFromDB = _unitOfWork.Playlists.Get(playlistId);
            var mappedPlaylist = _mapper.Map<PlaylistDto>(playlistFromDB);
            return mappedPlaylist;
        }

        public void UpdatePlaylist(int playlistId, PlaylistUpdateDto playlistToUpdate)
        {
            var playlist = _unitOfWork.Playlists.Get(playlistId);
            playlist.Name = playlistToUpdate.Name;

            foreach(int songId in playlistToUpdate.SongsId)
            {
                var song = playlist.Songs.FirstOrDefault(song => song.Id== songId);

                if (song == null)
                    playlist.Songs.Add(song);
            }

            _unitOfWork.Playlists.Update(playlist);
            _unitOfWork.Save();
        }
    }
}
