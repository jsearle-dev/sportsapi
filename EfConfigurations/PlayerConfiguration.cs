using Microsoft.EntityFrameworkCore;
using SportsApi.Models;

namespace SportsApi.EfConfigurations
{
    public class PlayerEntityConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Team).WithMany(t => t.Players).HasForeignKey(p => p.TeamId);
        }
    }
}