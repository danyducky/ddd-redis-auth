using Auth.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Configurations;

public class CredentialEntityTypeConfiguration : IEntityTypeConfiguration<Credential>
{
    public void Configure(EntityTypeBuilder<Credential> builder)
    {
        builder.HasKey(x => new {x.UserId, x.RoleId});

        builder.HasOne(x => x.User)
            .WithMany(x => x.Credentials)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Role)
            .WithMany(x => x.Credentials)
            .HasForeignKey(x => x.RoleId);
    }
}