
using DataLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class AlbumDto
    {
        public string Name { get; set; }
        public int AtristId { get; set; }
        public IEnumerable<SongUpdateDto> Songs { get; set; }
    }
}
