using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Services.Interface;
using DataLayer.Models;
using DataLayer.Repository.Interface;

namespace BusinessLayer.Services
{
    public class SongService : ISongService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SongService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Create(int id, string pathFile)
        {
            TagLib.File tfile = TagLib.File.Create(pathFile);
            string title = tfile.Tag.Title;
            string author = String.Join(", ", tfile.Tag.Performers);
            string duration = tfile.Properties.Duration.ToString("mm\\:ss");
            _unitOfWork.Song.Create(new Song
            {
                Name = title ?? "no name",
                Author = author ?? "no name",
                Time = duration,
                PlaylistId = id
            });
            _unitOfWork.Save();
            return true;
        }

        public bool Delete(int id)
        {
            _unitOfWork.Song.Delete(_unitOfWork.Song.Get(id));
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Song> GetAll(int id)
        {
            return _unitOfWork.Song.GetAll().Where(song => song.PlaylistId == id);
        }
    }
}
