using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendServer.Data.EF;

public class BHPNTDbContext : DbContext
{
    public BHPNTDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BranchConfiguration());
        modelBuilder.ApplyConfiguration(new InfoCBNVConfiguration());
        modelBuilder.ApplyConfiguration(new AnnexContractConfiguration());
        modelBuilder.ApplyConfiguration(new CollateralConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new InsuranceContractConfiguration());
        modelBuilder.ApplyConfiguration(new PartnerConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentPeriodConfiguration());
    }

    public DbSet<AnnexContract> AnnexContracts { set; get; } = default!;
    public DbSet<Branch> Branches { set; get; } = default!;
    public DbSet<Collateral> Collaterals { set; get; } = default!;
    public DbSet<Customer> Customers { set; get; } = default!;
    public DbSet<InfoCBNV> InfoCBNVs { set; get; } = default!;
    public DbSet<InsuranceContract> InsuranceContracts { set; get; } = default!;
    public DbSet<Partner> Partners { set; get; } = default!;
    public DbSet<PaymentPeriod> PaymentPeriods { set; get; } = default!;
}