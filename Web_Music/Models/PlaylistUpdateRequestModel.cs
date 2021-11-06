namespace Web_Music.Models
{
    public class PlaylistUpdateRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public UserResponseModel UserResponseModel { get; set; }
    }
}
