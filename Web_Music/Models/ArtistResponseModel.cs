using DataLayer.Models;
using System.Collections.Generic;

namespace Web_Music.Models
{
    public class ArtistResponseModel
    {
        public string Name { get; set; }
        public IEnumerable<Song> Songs { get; set; }
        public IEnumerable<Album> Albums { get; set; }
    }
}
