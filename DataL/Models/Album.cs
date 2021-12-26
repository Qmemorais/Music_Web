using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Album
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        public DateTime Time { get; set; }
        [Required]
        public int AtristId { get; set; }
        public virtual List<Song> Songs { get; set; } = new List<Song>(); 
    }
}
