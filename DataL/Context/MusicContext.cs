using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // использование Fluent API
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistConfiguration());
            modelBuilder.ApplyConfiguration(new SongConfiguration());
        }
        public class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.HasAlternateKey(u => u.Email);
            }
        }
        public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
        {
            public void Configure(EntityTypeBuilder<Playlist> builder)
            {
                builder.Property(b => b.Name).IsRequired();
            }
        }
        public class SongConfiguration : IEntityTypeConfiguration<Song>
        {
            public void Configure(EntityTypeBuilder<Song> builder)
            {
            }
        }
    }
}
