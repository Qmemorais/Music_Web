using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    [Index("Name", IsUnique = true)]
    public class Playlist
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public Guid OwnerUserId { get; set; }
        public virtual List<User> Users { get; set; } = new List<User>();
        public virtual List<Song> Songs { get; set; } = new List<Song>();
    }
}
