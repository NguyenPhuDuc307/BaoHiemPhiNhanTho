using BackendServer.Data.EF;

namespace BackendServer.Data;

public class DbInitializer
{
    private readonly BHPNTDbContext _context;

    public DbInitializer(BHPNTDbContext context)
    {
        _context = context;
    }

    public async Task Seed()
    {
        await _context.SaveChangesAsync();
    }

}