﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataL
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public List<Song> Songs { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
