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
                _unitOfWork.Songs.Create(createSong);
                _unitOfWork.Save();
            }
        }

        public void DeleteSong(int songId)
        {
            _unitOfWork.Songs.Delete(songId);
            _unitOfWork.Save();
        }

        public IEnumerable<SongDto> GetAllSongsByPlaylist(int playlistId)
        {//aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
            //переделать эту фигню
            var playlistToGetSongs = _unitOfWork.Playlists.Get(playlistId);
            var songFromPlaylist = _unitOfWork.Songs.GetAll().Where(song => song.Playlists.Contains(playlistToGetSongs));
            var mappedSongs = _mapper.Map<IEnumerable<SongDto>>(songFromPlaylist);
            return mappedSongs;
        }

        public IEnumerable<SongDto> GetAllSongs()
        {//aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
            //добавить чтобы выводило спиок плейлистов
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
            
            foreach(Playlist playlist in songToUpdate.Playlists)
            {
                var playlistToGet = song.Playlists.FirstOrDefault(playlist => playlist.Id == playlist.Id);

                if (playlistToGet == null)
                    song.Playlists.Add(playlistToGet);
            }

            _unitOfWork.Songs.Update(song);
            _unitOfWork.Save();
        }

        public IEnumerable<SongDto> GetAllSongsByArtist(int artistId)
        {
            var songFromArtist = _unitOfWork.Songs.GetAll().Where(song => song.ArtistId==artistId);
            var mappedSongs = _mapper.Map<IEnumerable<SongDto>>(songFromArtist);
            return mappedSongs;
        }

        public IEnumerable<SongDto> GetAllSongsByAlbum(int albumId)
        {
            var songFromAlbum = _unitOfWork.Songs.GetAll().Where(song => song.AlbumId == albumId);
            var mappedSongs = _mapper.Map<IEnumerable<SongDto>>(songFromAlbum);
            return mappedSongs;
        }
    }
}
