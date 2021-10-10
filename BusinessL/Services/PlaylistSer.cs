using DataL;
using System.Linq;

namespace BusinessL.Services
{
    class PlaylistSer
    {
        public PlaylistSer() { pctx = new PlaylistContext(); }
        public PlaylistContext pctx { get; set; }
        public bool CreateNewPlaylist(User user, string login, string name)
        {
            if (pctx.Playlists.FirstOrDefault(playlist => playlist.Name == name && playlist.User.Login == login) == null)
            {
                pctx.Playlists.Add(new Playlist { Name = name, User = user });
                pctx.SaveChanges();
                return true;
            }
            else
                return true;
        }
        public bool RenamePlaylist(string login, string oldName, string newName)
        {
            if (pctx.Playlists.FirstOrDefault(playlist => playlist.Name == newName && playlist.User.Login == login) == null)
            {
                Playlist playlist_ = pctx.Playlists.FirstOrDefault(playlist => playlist.Name == oldName);
                playlist_.Name = newName;
                pctx.Playlists.Update(playlist_);
                pctx.SaveChanges();
                return true;
            }
            else return false;

        }
        public bool DeletePlaylist(string login, string name)
        {
            Playlist playlist_ = pctx.Playlists.FirstOrDefault(playlist => playlist.Name == name && playlist.User.Login == login);
            pctx.Playlists.Remove(playlist_);
            pctx.SaveChanges();
            return false;
        }
    }
}
