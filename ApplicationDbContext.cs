using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;

namespace ADOMovieDatabaseConsoleAppV3.Models
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Movie>? Movies { get; set; }
        public DbSet<Genre>? Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var folder = Environment.SpecialFolder.LocalApplicationData;
            var appDataPath = Environment.GetFolderPath(folder);
            var dbPath = Path.Combine(appDataPath, configuration["DbFilename"]);

            optionsBuilder
                .UseSqlite($"DataSource={dbPath}")
                .EnableSensitiveDataLogging()
                .LogTo(x => Debug.WriteLine(x));
        }
    }
}