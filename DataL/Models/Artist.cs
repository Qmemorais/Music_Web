using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    [Index("Name", IsUnique = true)]
    public class Artist
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        public virtual List<Song> Songs { get; set; } = new List<Song>();
        public virtual List<Album> Albums { get; set; } = new List<Album>();
    }
}
