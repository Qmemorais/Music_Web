using System;

namespace Web_Music.Models
{
    public class PlaylistModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeToPlay { get; set; }
    }
}
