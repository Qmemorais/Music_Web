using System;

namespace Web_Music
{
    public class Song
    {
        public TimeSpan time_to_play { get; set; }
        public string author { get; set; }
        public string path{ get; set; }
        public string genre{ get; set; }
    }
}