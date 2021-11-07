using BusinessLayer.Models;
using DataLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interface
{
    public interface ISongService
    {
        public void CreateSong(SongCreateDto songToCreate);
        public void DeleteSong(int songId);
        public IEnumerable<SongDto> GetAllSongsByPlaylist(int playlistId);
    }
}