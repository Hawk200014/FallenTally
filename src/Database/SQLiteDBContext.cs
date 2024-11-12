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
        public DbSet<SettingsModel> Settings { get; set; }
        public DbSet<GameStatsModel> GameStats { get; set; }
        public DbSet<DeathModel> Deaths { get; set; }
        public DbSet<DeathLocationModel> Locations { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!File.Exists(GLOBALVARS.PATHTODB)) File.Create(GLOBALVARS.PATHTODB);
            optionsBuilder.UseSqlite("Data Source=" + GLOBALVARS.PATHTODB);
        }

    }
}
