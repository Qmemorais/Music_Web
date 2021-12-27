using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Song
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get ; set ; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public Guid ArtistId { get; set; }
        [Required]
        public Guid AlbumId { get; set; }
        public virtual List<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
