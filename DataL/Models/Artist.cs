using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Artist
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<Song> Songs { get; set; }
        public virtual List<Album> Albums { get; set; }
    }
}
