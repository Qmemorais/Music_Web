﻿using System;

namespace Web_Music.Models
{
    public class PlaylistCreateRequestModel
    {
        public string Name { get; set; }
        public Guid UserOwnerId { get; set; }
    }
}
