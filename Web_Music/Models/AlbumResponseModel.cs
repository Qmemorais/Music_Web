﻿using System;
using System.Collections.Generic;

namespace Web_Music.Models
{
    public class AlbumResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public Guid AtristId { get; set; }
        public List<SongModel> Songs { get; set; }
    }
}
