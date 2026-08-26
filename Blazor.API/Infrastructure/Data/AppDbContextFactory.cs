using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Blazor.API.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Usa SQLite para migrations
        optionsBuilder.UseSqlite("Data Source=blazormvvm.db");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}