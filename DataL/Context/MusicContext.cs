using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class MusicContext : DbContext
    {
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }

        public MusicContext() { }
        public MusicContext(DbContextOptions<MusicContext> options)
    : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=musicdb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                    .HasMany(c => c.Playlists)
                    .WithMany(s => s.Users)
                    .UsingEntity(j => j.ToTable("UserPlaylist"));
            modelBuilder.Entity<Playlist>()
                    .HasMany(c => c.Songs)
                    .WithMany(s => s.Playlists)
                    .UsingEntity(j => j.ToTable("PlaylistSong"));
        }
    }
}
