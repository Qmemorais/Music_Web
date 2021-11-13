using System.Collections.Generic;

namespace DataLayer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public virtual List<Playlist> Playlists { get; set; }
    }
}
