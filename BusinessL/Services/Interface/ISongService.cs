using DataLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interface
{
    public interface ISongService
    {
        bool Create(int id, string name);
        bool Delete(int id);
        IEnumerable<Song> GetAll(int id);
    }
}