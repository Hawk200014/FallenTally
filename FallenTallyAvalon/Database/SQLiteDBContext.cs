using FallenTally.Database.Models;
using FallenTally.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace FallenTally.Database
{
    public class SQLiteDBContext : DbContext
    {
        public SQLiteDBContext()
        {
            TryMigrate();
        }

        public SQLiteDBContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<SettingsModel> Settings { get; set; }
        public virtual DbSet<GameStatsModel> GameStats { get; set; }
        public virtual DbSet<DeathModel> Deaths { get; set; }
        public virtual DbSet<DeathLocationModel> Locations { get; set; }
        public virtual DbSet<MarkerModel> Markers { get; set; }
        public virtual DbSet<RecordingModel> Recordings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string pathToDb = GLOBALVARS.PATHTOEXE;
                pathToDb = Path.Join(pathToDb, "Data");
                Directory.CreateDirectory(pathToDb);
                pathToDb = Path.Join(pathToDb, "data.db");
                if (!File.Exists(pathToDb)) File.Create(pathToDb).Close();
                optionsBuilder.UseSqlite("Data Source=" + pathToDb);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SettingsModel>()
                .HasKey(x => x.SettingsId);
            modelBuilder.Entity<GameStatsModel>()
                .HasKey(x => x.GameId);
            modelBuilder.Entity<DeathModel>()
                .HasKey(x => x.DeathId);
            modelBuilder.Entity<DeathLocationModel>()
                .HasKey(x => x.LocationId);
            modelBuilder.Entity<MarkerModel>()
                .HasKey(x => x.MarkerId);
            modelBuilder.Entity<RecordingModel>()
                .HasKey(x => x.RecordingId);
        }

        public void TryMigrate()
        {
            Database?.GetService<IMigrator>()?.Migrate(); // Replace Database.Migrate() with this
        }
    }
}
