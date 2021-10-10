using System;
using System.Linq;
using DataL;

namespace BusinessL.Services
{
    public class SongServ
    {
        public SongServ() { sctx = new SongsContext(); }
        public SongsContext sctx { get; set; }
        public bool AddNewSong(int playlist, string pathMP3 )
        {
            if(sctx.Songs.FirstOrDefault(song => song.PlaylistName==playlist)!=null)
            {
                TagLib.File tfile = TagLib.File.Create(pathMP3);
                string title = tfile.Tag.Title;
                string author = String.Join(", ", tfile.Tag.Performers);
                string duration = tfile.Properties.Duration.ToString("mm\\:ss");
                sctx.Songs.Add(new Song { PlaylistName = playlist, Path = pathMP3, Name = title, Author = author, Time = duration });
                sctx.SaveChanges();
                return true;
            }
            else
                return true;
        }
        public bool DeleteSong(int playlist, int idSong)
        {
            Song deleteSong = sctx.Songs.FirstOrDefault(song => song.PlaylistName == playlist && song.Id == idSong);
            sctx.Songs.Remove(deleteSong);
            sctx.SaveChanges();
            return false;
        }
        public void PlaySong(string MP3)
        {//aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
        }
    }
}
