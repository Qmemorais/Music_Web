using System;
using System.Collections.Generic;
using BusinessLayer.Models;

namespace BusinessLayer.Services.Interfaces
{
    public interface ISongService
    {
        public void CreateSong(SongCreateDTO songCreate);
        public void UpdateSong(SongUpdateDTO songUpdate, Guid id);
        public void DeleteSong(Guid id);
        public List<SongDTOToGet> GetSongs();
        public SongDTOToGet GetSongById(Guid id);
        public List<SongDTOToGet> GetAllSongsByPlaylist(Guid playlistId);
        public List<SongDTOToGet> GetAllSongsByArtist(Guid artistId);
        public List<SongDTOToGet> GetAllSongsByAlbum(Guid AlbumId);
        //get all songs which have the same time to play
        public List<SongDTOToGet> GetAllSongsByTime(DateTime time);
    }
}
