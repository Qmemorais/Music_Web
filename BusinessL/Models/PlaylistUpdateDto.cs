
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class PlaylistUpdateDto
    {
        public string Name { get; set; }
        public IEnumerable<int> UsersId { get; set; }
        public IEnumerable<int> SongsId { get; set; }

    }
}
