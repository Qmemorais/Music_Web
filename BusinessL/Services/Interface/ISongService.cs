using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interface
{
    public interface ISongService
    {
        public void CreateSong(SongCreateDto songToCreate);
        public void UpdateSong(int songId, SongUpdateDto songToUpdate);
        public void DeleteSong(int songId);
        public IEnumerable<SongDto> GetAllSongs();
        public IEnumerable<SongDto> GetAllSongsByPlaylist(int playlistId);
        public SongDto GetSongById(int songId);
    }
}