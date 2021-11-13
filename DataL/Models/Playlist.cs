﻿using System.Collections.Generic;

namespace DataLayer.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<User> Users { get; set; }
        public virtual List<Song> Songs { get; set; }
    }
}
