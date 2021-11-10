using System.Collections.Generic;

namespace DataLayer.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Song> Songs { get; set; }
        public virtual IEnumerable<Album> Albums { get; set; }
    }
}
