using BusinessLayer.Models;

namespace BusinessLayer.Services.Interface
{
    public interface ISongService
    {
        public void CreateSong(SongCreateDto songToCreate);
        public void UpdateSong(int songId, SongUpdateDto songToUpdate);
        public void DeleteSong(int songId);
        public SongDto GetSongById(int songId);
    }
}