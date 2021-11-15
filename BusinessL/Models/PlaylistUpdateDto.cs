
using DataLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class PlaylistUpdateDto
    {
        public string Name { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Song> Songs { get; set; }

    }
}
