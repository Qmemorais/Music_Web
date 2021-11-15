using DataLayer.Models;
using System.Collections.Generic;

namespace Web_Music.Models
{
    public class AlbumResponseModel
    {
        public string Name { get; set; }
        public int AtristId { get; set; }
        public IEnumerable<Song> Songs { get; set; }
    }
}
