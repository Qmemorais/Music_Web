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
        public void Create(SongCreateDto songToCreate)
        {
            var createSong = _mapper.Map<Song>(songToCreate);
            _unitOfWork.Songs.Create(createSong);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Songs.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<SongUpdateDto> GetAll(int id)
        {
            var songFromDB = _unitOfWork.Songs.GetAll().Where(song => song.PlaylistId == id);
            var songs = _mapper.Map<IEnumerable<SongUpdateDto>>(songFromDB);
            return songs;
        }
    }
}
