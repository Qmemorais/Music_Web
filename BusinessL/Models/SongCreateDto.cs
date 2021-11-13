
namespace BusinessLayer.Models
{
    public class SongCreateDto
    {
        public string Name { get; set; }
        public string Time { get; set; }
        public int ArtistId { get; set; }
        public int AlbumId { get; set; }
    }
}
