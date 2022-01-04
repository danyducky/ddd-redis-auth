using Auth.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Configurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        // builder.Property(x => x.Id).UseHiLo("userseq");

        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.RegistrationDate).IsRequired();

        builder.OwnsOne(x => x.Personal);

        builder.HasMany(x => x.Credentials)
            .WithOne(x => x.User);
    }
}