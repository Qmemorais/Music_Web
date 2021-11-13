using System.Collections.Generic;
namespace BusinessLayer.Models
{
    public class ArtistUpdateDto
    {
        public string Name { get; set; }
        public IEnumerable<int> SongsId { get; set; }
        public IEnumerable<int> AlbumsId { get; set; }
    }
}
