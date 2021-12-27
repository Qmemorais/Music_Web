using System;

namespace BusinessLayer.Models
{
    public class SongUpdateDTO
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime Time { get; set; }
    }
}
