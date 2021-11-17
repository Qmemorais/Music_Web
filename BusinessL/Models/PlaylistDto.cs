
using DataLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class PlaylistDto
    {
        public string Name { get; set; }
        public IEnumerable<UserUpdateDto> Users { get; set; }
        public IEnumerable<SongUpdateDto> Songs { get; set; }
    }
}
