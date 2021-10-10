using System;
using System.Collections.Generic;

namespace Web_Music
{
    public class User
    {
        public List<PlayList> list_of_playlists { get; set; }
        public string login { get; set; }
        public string passw{ get; set; }
    }
}