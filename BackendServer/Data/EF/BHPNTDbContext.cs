using Microsoft.EntityFrameworkCore;

namespace BackendServer.Data.EF;

public class BHPNTDbContext : DbContext
{
    public BHPNTDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

    // public DbSet<User> Users { set; get; } = default!;
}