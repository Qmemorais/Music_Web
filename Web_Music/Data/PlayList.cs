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
        public bool add_to_playlist()
        {//���������� ����� � ��������
            return false;
        }
        public bool remove_from_playlist()
        {//������ ������� ����� �����
            playlist.RemoveAt(2);
            return false;
        }
        public bool rename_playlist(string new_name)
        {//�������������� ���������
            this.name_of_playlist = new_name;
            return false;
        }
        //����� � ���������� �� �����-�� ���������
    }
}