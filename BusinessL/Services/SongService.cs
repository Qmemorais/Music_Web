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
            _unitOfWork.Songs.Create(createSong);
            _unitOfWork.Save();
        }

        public void DeleteSong(int songId)
        {
            _unitOfWork.Songs.Delete(songId);
            _unitOfWork.Save();
        }

        public IEnumerable<SongDto> GetAllSongsByPlaylist(int playlistId)
        {
            var songFromDB = _unitOfWork.Songs.GetAll().Where(song => song.PlaylistId == playlistId);
            var songs = _mapper.Map<IEnumerable<SongDto>>(songFromDB);
            return songs;
        }
    }
}
