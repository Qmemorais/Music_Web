using System;
using System.Collections.Generic;

namespace Web_Music.Models
{
    public class ArtistResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public List<SongModel> Songs { get; set; }
        public List<AlbumModel> Albums { get; set; }
    }
}
