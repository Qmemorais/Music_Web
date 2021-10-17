﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataL.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get ; set ; }
        public string Path { get; set; }
        public string Author { get; set; }
        public string Time { get; set; }
        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }

    }
}
