using System;

namespace BusinessLayer.Models
{
    public class PlaylistDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeToPlay { get; set; }
    }
}
