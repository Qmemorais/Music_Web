using System;
using System.Collections.Generic;

namespace Web_Music.Models
{
    public class SongResponseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime Time { get; set; }
        public Guid ArtistId { get; set; }
        public Guid AlbumId { get; set; }
        public List<PlaylistModel> Playlists { get; set; }
    }
}
