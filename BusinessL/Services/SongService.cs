using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Services.Interface;
using BusinessLayer.Models;
using DataLayer.Models;
using DataLayer.UnitOfWork;
using AutoMapper;

namespace BusinessLayer.Services
{
    public class SongService : ISongService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public SongService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void CreateSong(SongCreateDto songToCreate)
        {
            var createSong = _mapper.Map<Song>(songToCreate);
            var isSongExist = _unitOfWork.Songs.GetAll().Any(song => song.Equals(createSong));

            if(!isSongExist)
            {
                var song = _unitOfWork.Songs.Create(createSong);
                var artist = _unitOfWork.Artists.Get(songToCreate.ArtistId);
                artist.Songs.Add(song);
                var album = _unitOfWork.Albums.Get(songToCreate.AlbumId);
                album.Songs.Add(song);
                _unitOfWork.Artists.Update(artist);
                _unitOfWork.Albums.Update(album);
                _unitOfWork.Save();
            }
        }

        public void DeleteSong(int songId)
        {
            _unitOfWork.Songs.Delete(songId);
            _unitOfWork.Save();
        }

        public IEnumerable<SongDto> GetAllSongsByPlaylist(int playlistId)
        {
            var playlistToGetSongs = _unitOfWork.Playlists.Get(playlistId);
            var songFromPlaylist = _unitOfWork.Songs.GetAll().Where(song => song.Playlists.Contains(playlistToGetSongs));
            var mappedSongs = _mapper.Map<IEnumerable<SongDto>>(songFromPlaylist);
            return mappedSongs;
        }

        public IEnumerable<SongDto> GetAllSongs()
        {
            var songFromDB = _unitOfWork.Songs.GetAll();
            var mappedSongs = _mapper.Map<IEnumerable<SongDto>>(songFromDB);
            return mappedSongs;
        }

        public SongDto GetSongById(int songId)
        {
            var songFromDB = _unitOfWork.Songs.Get(songId);
            var mappedSong = _mapper.Map<SongDto>(songFromDB);
            return mappedSong;
        }

        public void UpdateSong(int songId, SongUpdateDto songToUpdate)
        {
            var song = _unitOfWork.Songs.Get(songId);
            song.Name = songToUpdate.Name;
            
            foreach(int playlistId in songToUpdate.PlaylistsId)
            {
                var playlist = song.Playlists.FirstOrDefault(playlist => playlist.Id == playlistId);

                if (playlist == null)
                    song.Playlists.Add(playlist);
            }

            _unitOfWork.Songs.Update(song);
            _unitOfWork.Save();
        }
    }
}
