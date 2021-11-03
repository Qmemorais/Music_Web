namespace Web_Music.Models
{
    public class PlaylistCreateRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public UserCreateRequestModel UserCreateRequestModel { get; set; }
    }
}
