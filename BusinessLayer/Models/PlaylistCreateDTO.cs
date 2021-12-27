using System;

namespace BusinessLayer.Models
{
    public class PlaylistCreateDTO
    {
        public string Name { get; set; }
        public Guid UserOwnerId { get; set; }
    }
}
