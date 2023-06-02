using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BackendServer.Data.EF;

public class BHPNTDbContextFactory : IDesignTimeDbContextFactory<BHPNTDbContext>
{
    public BHPNTDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
    .AddJsonFile("appsettings.Development.json")
#else
    .AddJsonFile("appsettings.Production.json")
#endif
    .Build();

        var connectionString = configuration.GetConnectionString("BHPNTDb");

        var optionsBuilder = new DbContextOptionsBuilder<BHPNTDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new BHPNTDbContext(optionsBuilder.Options);
    }
}
