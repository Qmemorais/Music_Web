namespace Web_Music.Models
{
    public class SongUpdateRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Path { get; set; }
        //looking for authors
        public string Author { get; set; }
        public string Time { get; set; }
        //public string Genre { get; set; }
        public int PlaylistId { get; set; }
        public PlaylistUpdateRequestModel PlaylistUpdateRequestModel { get; set; }
    }
}
