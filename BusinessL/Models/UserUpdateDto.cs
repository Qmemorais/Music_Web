﻿
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class UserUpdateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public IEnumerable<int> PlaylistsId { get; set; }
    }
}
