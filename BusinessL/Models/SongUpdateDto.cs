
namespace BusinessLayer.Models
{
    public class SongUpdateDto
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Time { get; set; }
        public int AtristId { get; set; }
        public int AlbumId { get; set; }
    }
}
