using DeathCounterHotkey.Database.Models;
using DeathCounterHotkey.Resources;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Database
{
    public class SQLiteDBContext : DbContext
    {
        public SQLiteDBContext() 
        {
            //Database.EnsureCreated();
            Database.Migrate();
        }
        public DbSet<SettingsModel> Settings { get; set; }
        public DbSet<GameStatsModel> GameStats { get; set; }
        public DbSet<DeathModel> Deaths { get; set; }
        public DbSet<DeathLocationModel> Locations { get; set; }
        public DbSet<MarkerModel> Markers { get; set; }
        public DbSet<RecordingModel> Recordings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string pathToDb = GLOBALVARS.PATHTOEXE;
            pathToDb = Path.Join(pathToDb, "Data");
            Directory.CreateDirectory(pathToDb);
            pathToDb = Path.Join(pathToDb, "data.db");
            if (!File.Exists(pathToDb)) File.Create(pathToDb).Close();
            optionsBuilder.UseSqlite("Data Source=" + pathToDb);
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
            //Database.Migrate();
        }
    }
}
