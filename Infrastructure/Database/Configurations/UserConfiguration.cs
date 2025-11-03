using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("Id");

        builder.Property(u => u.Username).HasColumnName("Username").IsRequired().HasMaxLength(255);
        builder.Property(u => u.Password).HasColumnName("Password").IsRequired();

        builder.HasIndex(u => u.Username).IsUnique();
    }
}