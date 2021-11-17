using System.Collections.Generic;
using DataLayer.Models;
namespace Web_Music.Models
{
    public class UserResponseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public IEnumerable<PlaylistUpdateRequestModel> Playlists { get; set; }
    }
}
