
using DataLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class SongUpdateDto
    {
        public string Name { get; set; }
        public string Time { get; set; }
        public int ArtistId { get; set; }
        public int AlbumId { get; set; }
    }
}
