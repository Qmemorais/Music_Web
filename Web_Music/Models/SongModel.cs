using System;

namespace Web_Music.Models
{
    public class SongModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime Time { get; set; }
    }
}
