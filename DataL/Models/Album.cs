using System.Collections.Generic;

namespace DataLayer.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AtristId { get; set; }
        public virtual List<Song> Songs { get; set; } = new List<Song>();
    }
}
