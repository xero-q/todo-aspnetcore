using Domain.Todos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable("Todos");

        builder.HasKey(t => t.TodoId);
        builder.Property(t => t.TodoId).HasColumnName("TodoId");

        builder.Property(t => t.Title).HasColumnName("Title").IsRequired().HasMaxLength(255);

        builder.Property(t => t.Description).HasColumnName("Description").IsRequired().HasMaxLength(255);

        builder.Property(t => t.DueDate).HasColumnName("DueDate").IsRequired();

        builder.Property(t => t.IsCompleted).HasColumnName("IsCompleted").IsRequired();
        
        builder.HasIndex(t => t.Title).IsUnique();
    }
}