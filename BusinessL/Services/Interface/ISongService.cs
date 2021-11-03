using DataLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interface
{
    public interface ISongService
    {
        public void Create(Song SongToCreate);
        public void Delete(int id);
        public IEnumerable<Song> GetAll(int id);
    }
}