using System.Collections.Generic;
using System.Linq;
using BusinessL.Services.Interface;
using DataL.Context;
using DataL.Models;

namespace BusinessL.Services
{
    public class SongRepository : IRepository<Song>,ISongRepository<Song>
    {
        private MusicContext db;

        public SongRepository(MusicContext context)
        {
            this.db = context;
        }
        public IEnumerable<Song> GetAll(int id)
        {
            return db.Songs.Where(song => song.PlaylistId == id);
        }

        public Song Get(int id)
        {
            return db.Songs.Find(id);
        }

        public void Create(Song song)
        {
            db.Songs.Add(song);
        }

        public void Delete(int id)
        {
            Song song = db.Songs.Find(id);
            if (song != null)
                db.Songs.Remove(song);
        }
    }
}
