using Microsoft.EntityFrameworkCore;
using MusicApp.Data.Entities;
using MusicApp.Data.Maps;

namespace MusicApp.Data
{
    public class MyDBContext : DbContext
    {
        public virtual DbSet<Song> Song { get; set; }
        public virtual DbSet<Artist> Artist { get; set; }
        public virtual DbSet<SongArtistMap> SongArtistMap { get; set; }
        public virtual DbSet<PlayList> PlayList { get; set; }   
        public virtual DbSet<PlayListSongMap> PlayListSongMap { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<TagSongMap> TagSongMap { get; set; }
        public virtual DbSet<MusicType> MusicType { get; set; }
        public virtual DbSet<MusicTypeArtistMap> MusicTypeArtistMap { get; set; }
        public virtual DbSet<MusicTypePlayListMap> MusicTypePlayListMap { get; set; }
        public virtual DbSet<MusicTypeSongMap> MusicTypeSongMap { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-TVANJ1T\SQLEXPRESS;Database=MusicPlayer;Trusted_Connection=True;TrustServerCertificate=true;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Song>();
            modelBuilder.Entity<Artist>();
            modelBuilder.Entity<SongArtistMap>();
            modelBuilder.Entity<PlayList>();
            modelBuilder.Entity<PlayListSongMap>();
            modelBuilder.Entity<Tag>();
            modelBuilder.Entity<TagSongMap>();
            modelBuilder.Entity<MusicType>();
            modelBuilder.Entity<MusicTypeArtistMap>();
            modelBuilder.Entity<MusicTypePlayListMap>();
            modelBuilder.Entity<MusicTypeSongMap>();
        }

    }
}
