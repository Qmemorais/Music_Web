using System;

namespace Web_Music.Models
{
    public class SongCreateRequestModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime Time { get; set; }
        public Guid ArtistId { get; set; }
        public Guid AlbumId { get; set; }

    }
}
