using System;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class ArtistDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public List<SongDTO> Songs { get; set; }
        public List<AlbumDTO> Albums { get; set; }
    }
}
