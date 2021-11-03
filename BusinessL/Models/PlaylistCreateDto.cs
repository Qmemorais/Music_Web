namespace BusinessLayer.Models
{
    public class PlaylistCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public UserCreateDto UserCreateDto { get; set; }
    }
}
