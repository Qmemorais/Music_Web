using System.Collections.Generic;

namespace Web_Music.Models
{
    public class ArtistResponseModel
    {
        public string Name { get; set; }
        public IEnumerable<int> SongsId { get; set; }
        public IEnumerable<int> AlbumsId { get; set; }
    }
}
