using System;
using System.Linq;
using DataL;

namespace BusinessL.Services
{
    public class SongServ
    {
        public SongServ() { sctx = new MusicContext(); }
        public MusicContext sctx { get; set; }
        public bool AddNewSong(int id, string pathMP3 )
        {
            if(sctx.Songs.FirstOrDefault(song => song.PlaylistId==id)!=null)
            {
                TagLib.File tfile = TagLib.File.Create(pathMP3);
                string title = tfile.Tag.Title;
                string author = String.Join(", ", tfile.Tag.Performers);
                string duration = tfile.Properties.Duration.ToString("mm\\:ss");
                sctx.Songs.Add(new Song { PlaylistId = id, Path = pathMP3, Name = title, Author = author, Time = duration });
                sctx.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool DeleteSong(int id, int idSong)
        {
            Song deleteSong = sctx.Songs.FirstOrDefault(song => song.PlaylistId == id && song.Id == idSong);
            sctx.Songs.Remove(deleteSong);
            sctx.SaveChanges();
            return true;
        }
        public void PlaySong(string MP3)
        {//aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
        }
    }
}
