using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<User> Users { get; set; }
        public virtual List<Song> Songs { get; set; }
    }
}
