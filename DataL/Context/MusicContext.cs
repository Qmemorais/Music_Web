using Microsoft.EntityFrameworkCore;
using DataL.Models;

namespace DataL.Context
{
    public class MusicContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<User> Users { get; set; }
        public MusicContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=musicAPIdb;Trusted_Connection=True;");
        }
    }
}
