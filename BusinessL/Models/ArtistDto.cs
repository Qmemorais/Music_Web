
using DataLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class ArtistDto
    {
        public string Name { get; set; }
        public List<SongUpdateDto> Songs { get; set; }
        public List<AlbumUpdateDto> Albums { get; set; }

    }
}
