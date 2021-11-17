
using DataLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public IEnumerable<PlaylistUpdateDto> Playlists { get; set; }
    }
}
