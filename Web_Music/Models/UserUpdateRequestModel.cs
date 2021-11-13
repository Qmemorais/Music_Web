
using System.Collections.Generic;

namespace Web_Music.Models
{
    public class UserUpdateRequestModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public IEnumerable<int> PlaylistsId { get; set; }
    }
}
