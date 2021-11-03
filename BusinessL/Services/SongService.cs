using System;
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
        public SongService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(SongCreateDto songToCreate)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SongCreateDto, Song>());
            var mapper = new Mapper(config);
            Song CreateSong = mapper.Map<SongCreateDto, Song>(songToCreate);
            _unitOfWork.Songs.Create(CreateSong);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Songs.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<SongUpdateDto> GetAll(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongUpdateDto>());
            var mapper = new Mapper(config);
            var songFromDB = _unitOfWork.Songs.GetAll().Where(song => song.PlaylistId == id);
            var songs = mapper.Map<List<SongUpdateDto>>(songFromDB);
            return songs;
        }
    }
}
