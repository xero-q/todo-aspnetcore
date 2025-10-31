using Microsoft.EntityFrameworkCore;
using Infrastructure.Database.Persistence;

namespace Tests;

public class TestDbContextFactory
{
    public static ApplicationDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var dbContext = new ApplicationDbContext(options);

        dbContext.Database.EnsureCreated();

        return dbContext;
    }
}
