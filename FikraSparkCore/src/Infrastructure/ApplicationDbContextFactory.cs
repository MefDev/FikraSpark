using FikraSparkCore.Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace FikraSparkCore.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    private readonly string  _connectionString = "Server=localhost,5434;Database=FikraSparkDb;User Id=sa;Password=Password123@KL@;TrustServerCertificate=True";
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseSqlServer(_connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}

