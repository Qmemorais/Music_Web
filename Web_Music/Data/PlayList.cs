using System;
using System.Collections.Generic;

namespace Web_Music
{
    public class PlayList
    {
        List<Song> playlist;
        private string name_of_playlist;
        public string Name_of_Playlist { get { return name_of_playlist; } set { name_of_playlist = value; } }

        public PlayList(string _name_of_playlist)
        {
            this.name_of_playlist = _name_of_playlist;
        }
        public bool add_to_playlist(string author, string path, string genre, TimeSpan time_to_play)
        {//add song to playlist
            playlist.Add(new Song(author, path, genre, time_to_play));
            return false;
        }
        public bool remove_from_playlist(int delete_song)
        {//remove where song is
            playlist.RemoveAt(delete_song);
            return false;
        }
        public bool rename_playlist(string new_name)
        {//rename playlist
            this.name_of_playlist = new_name;
            return false;
        }
        //try to mashup and sort songs in playlist
    }
}