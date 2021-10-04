using System;
using System.Collections.Generic;

namespace Web_Music
{
    public class User
    {
        List<PlayList> list_of_playlists;
        private string login, passw;
        public User(string _login, string _passw)
        {//create
            this.login = _login;
            this.passw = _passw;
        }
        public bool create_playlist(string new_playlist)
        {
            list_of_playlists.Add(new PlayList(new_playlist));
            return false;
        }
        public bool delete_playlist(int index_to_delete)
        {//remove by name
            list_of_playlists.RemoveAt(index_to_delete);
            return false;
        }
        public bool re_passw(string new_passw)
        {//reload passw
            this.passw = new_passw;
            return false;
        }
    }
}