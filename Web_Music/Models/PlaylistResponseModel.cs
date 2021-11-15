using DataLayer.Models;
using System.Collections.Generic;

namespace Web_Music.Models
{
    public class PlaylistResponseModel
    {
        public string Name { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Song> Songs { get; set; }
    }
}
