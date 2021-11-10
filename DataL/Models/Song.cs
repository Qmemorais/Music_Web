using System.Collections.Generic;

namespace DataLayer.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get ; set ; }
        public string Time { get; set; }
        public int AtristId { get; set; }
        public int AlbumId { get; set; }
        public virtual IEnumerable<Playlist> Playlists { get; set; }
    }
}
