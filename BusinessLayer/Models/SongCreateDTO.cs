﻿using System;

namespace BusinessLayer.Models
{
    public class SongCreateDTO
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime Time { get; set; }
        public Guid AuthorId { get; set; }
        public Guid AlbumId { get; set; }
    }
}
