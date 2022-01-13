using System;
using System.Collections.Generic;

namespace Web_Music.Models
{
    public class UserResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public List<PlaylistModel> Playlists { get; set; }
    }
}
