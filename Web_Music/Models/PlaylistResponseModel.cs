﻿using System.Collections.Generic;

namespace Web_Music.Models
{
    public class PlaylistResponseModel
    {
        public string Name { get; set; }
        public IEnumerable<int> UsersId { get; set; }
        public IEnumerable<int> SongsId { get; set; }
    }
}