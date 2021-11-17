using System.Collections.Generic;
using DataLayer.Models;

namespace Web_Music.Models
{
    public class SongResponseModel
    {
        public string Name { get; set; }
        public string Time { get; set; }
        public int ArtistId { get; set; }
        public int AlbumId { get; set; }
        public IEnumerable<PlaylistUpdateRequestModel> Playlists { get; set; }
    }
}
