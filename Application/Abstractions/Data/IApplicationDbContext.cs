using Domain.Todos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    public DbSet<Todo> Todos { get; set; }
    public DbSet<User> Users { get; set; }
}
