using DataLayer.Models;
using System.Collections.Generic;

namespace Web_Music.Models
{
    public class PlaylistResponseModel
    {
        public string Name { get; set; }
        public IEnumerable<UserUpdateRequestModel> Users { get; set; }
        public IEnumerable<SongUpdateRequestModel> Songs { get; set; }
    }
}
