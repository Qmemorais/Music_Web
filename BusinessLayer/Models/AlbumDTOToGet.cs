using System;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class AlbumDTOToGet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public Guid AtristId { get; set; }
        public List<SongDTO> Songs { get; set; }
    }
}
