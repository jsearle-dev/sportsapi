using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using SportsApi.Models;
using SportsApi.EfConfigurations;

namespace SportsApi.Contexts
{
    public class SportsContext : DbContext
    {
        public SportsContext(DbContextOptions<SportsContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new TeamEntityConfiguration().Configure(modelBuilder.Entity<Team>());
            new PlayerEntityConfiguration().Configure(modelBuilder.Entity<Player>());
        }

        public DbSet<Team> Team { get; set; } = null!;
        public DbSet<Player> Player { get; set; } = null!;
    }
}