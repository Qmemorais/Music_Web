﻿using System.Collections.Generic;

namespace Web_Music.Models
{
    public class AlbumUpdateRequestModel
    {
        public string Name { get; set; }
        public int AtristId { get; set; }
        public IEnumerable<int> SongsId { get; set; }
    }
}
