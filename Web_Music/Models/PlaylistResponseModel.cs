using System;
using System.Collections.Generic;

namespace Web_Music.Models
{
    public class PlaylistResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserOwnerId { get; set; }
        public DateTime TimeToPlay { get; set; }
        public List<UserModel> Users { get; set; }
        public List<SongModel> Songs { get; set; }
    }
}
