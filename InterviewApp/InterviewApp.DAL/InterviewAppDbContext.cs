using System;
using InterviewApp.DAL.Entities.Watchlist;
using InterviewApp.DAL.EntityConfiguration.Watchlist;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InterviewApp.DAL
{
    public sealed class InterviewAppDbContext : DbContext
    {
        public DbSet<WatchlistItem> WatchlistItems { get; set; }

        public InterviewAppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.LogTo(log =>
            {
                Console.WriteLine($"{log}{Environment.NewLine}");
            }, new[] { RelationalEventId.CommandExecuted });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new WatchlistConfiguration());
        }
    }
}
