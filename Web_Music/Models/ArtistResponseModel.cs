using DataLayer.Models;
using System.Collections.Generic;

namespace Web_Music.Models
{
    public class ArtistResponseModel
    {
        public string Name { get; set; }
        public List<SongUpdateRequestModel> Songs { get; set; }
        public List<AlbumUpdateRequestModel> Albums { get; set; }
    }
}
