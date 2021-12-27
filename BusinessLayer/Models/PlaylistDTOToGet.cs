﻿using System;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class PlaylistDTOToGet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserOwnerId { get; set; }
        public DateTime TimeToPlay { get; set; }
        public List<UserDTO> Users { get; set; }
        public List<SongDTO> Songs { get; set; }
    }
}