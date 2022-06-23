using Microsoft.EntityFrameworkCore;
using SportsApi.Models;

namespace SportsApi.EfConfigurations
{
    public class TeamEntityConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasMany(t => t.Players).WithOne(t => t.Team).HasForeignKey(p => p.TeamId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}