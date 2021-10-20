using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repository
{
    public class GetListPlaylistRepository : IGetListRepository<Playlist>
    {
        private MusicContext db;

        public GetListPlaylistRepository(MusicContext context)
        {
            this.db = context;
        }

        public IEnumerable<Playlist> GetAll(int id)
        {
            return db.Playlists.Where(playlist => playlist.UserId==id);
        }
    }
}
