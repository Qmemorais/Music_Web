
using DataLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class PlaylistDto
    {
        public string Name { get; set; }
        public List<UserUpdateDto> Users { get; set; }
        public List<SongUpdateDto> Songs { get; set; }
    }
}
