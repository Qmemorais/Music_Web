using DataLayer.Models;
using System.Collections.Generic;
namespace BusinessLayer.Models
{
    public class ArtistUpdateDto
    {
        public string Name { get; set; }
        public IEnumerable<Song> Songs { get; set; }
        public IEnumerable<Album> Albums { get; set; }
    }
}
