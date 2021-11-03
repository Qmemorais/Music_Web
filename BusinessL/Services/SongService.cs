using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Services.Interface;
using DataLayer.Models;
using DataLayer.UnitOfWork;

namespace BusinessLayer.Services
{
    public class SongService : ISongService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SongService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Song SongToCreate)
        {
            _unitOfWork.Songs.Create(SongToCreate);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Songs.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<Song> GetAll(int id)
        {
            return _unitOfWork.Songs.GetAll().Where(song => song.PlaylistId == id);
        }
    }
}
