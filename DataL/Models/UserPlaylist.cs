
namespace DataLayer.Models
{
    public class UserPlaylist
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public virtual Playlist Playlist { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
