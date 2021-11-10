﻿using System.Collections.Generic;

namespace DataLayer.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<User> Users { get; set; }
        public virtual IEnumerable<Song> Songs { get; set; }
    }
}
