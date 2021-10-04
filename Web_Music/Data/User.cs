using System;
using System.Collections.Generic;

namespace Web_Music
{
    public class User
    {
        List<PlayList> list_of_playlist;
        private string login, passw;
        public User(string _login, string _passw)
        {//create
            this.login = _login;
            this.passw = _passw;
        }
        public bool create_playlist()
        {
            list_of_playlist.Add(new PlayList("sdf"));
            return false;
        }
        public bool delete_playlist()
        {//remove by name
            list_of_playlist.RemoveAt(0);
            return false;
        }
        public bool re_passw(string new_passw)
        {//reload passw
            this.passw = new_passw;
            return false;
        }
    }
}