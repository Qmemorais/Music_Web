using System;
using System.Collections.Generic;

namespace Web_Music
{
    public class User
    {
        List<PlayList> list_of_playlists { get; set; }
        private string login { get; set; }
        private string passw{ get; set; }
    }
}