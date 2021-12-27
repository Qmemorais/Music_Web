using System;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class UserDTOToGet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get;set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        List<PlaylistDTOToGive> Playlists { get; set; }
    }
}
