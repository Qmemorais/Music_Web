using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repository
{
    public class GetListSongRepository : IGetListRepository<Song>
    {
        private MusicContext db;

        public GetListSongRepository(MusicContext context)
        {
            this.db = context;
        }
        public IEnumerable<Song> GetAll(int id)
        {
            return db.Songs.Where(song => song.PlaylistId == id);
        }
    }
}
