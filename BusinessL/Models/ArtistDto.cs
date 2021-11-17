
using DataLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class ArtistDto
    {
        public string Name { get; set; }
        public IEnumerable<SongUpdateDto> Songs { get; set; }
        public IEnumerable<AlbumUpdateDto> Albums { get; set; }

    }
}
