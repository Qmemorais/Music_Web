namespace BusinessLayer.Models
{
    public class PlaylistUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public UserUpdateDto UserUpdateDto { get; set; }
    }
}
