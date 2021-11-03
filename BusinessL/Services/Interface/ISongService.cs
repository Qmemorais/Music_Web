using BusinessLayer.Models;
using DataLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interface
{
    public interface ISongService
    {
        public void Create(SongCreateDto songToCreate);
        public void Delete(int id);
        public IEnumerable<SongUpdateDto> GetAll(int id);
    }
}