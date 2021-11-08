using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class MusicContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<User> Users { get; set; }
        public MusicContext(DbContextOptions<MusicContext> options)
    : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=musicdb;Trusted_Connection=True;");
        }
    }
}
