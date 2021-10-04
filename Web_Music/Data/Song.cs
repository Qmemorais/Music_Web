using System;

namespace Web_Music
{
    public class Song
    {
        private TimeSpan time_to_play;
        private string author, path, genre;
        public Song(string _author, string _path, string _genre, TimeSpan _time_to_play)
        {
            this.author = _author; this.path = _path;
            this.genre = _genre; this.time_to_play = _time_to_play;
        }
    }
}