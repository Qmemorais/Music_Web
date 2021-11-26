using DataLayer.Models;
using System.Collections.Generic;

namespace Web_Music.Models
{
    public class PlaylistResponseModel
    {
        public string Name { get; set; }
        public List<UserUpdateRequestModel> Users { get; set; }
        public List<SongUpdateRequestModel> Songs { get; set; }
    }
}
