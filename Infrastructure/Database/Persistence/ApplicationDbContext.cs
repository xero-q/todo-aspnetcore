using Application.Abstractions.Data;
using Domain.Todos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    
    public DbSet<Todo> Todos { get; set; }
    public DbSet<User> Users { get; set; }
}