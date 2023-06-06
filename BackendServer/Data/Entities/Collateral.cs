using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaoHiemPhiNhanTho.BackendServer.Models;

public class Collateral
{
    public string? Ref { get; set; }
    public string? StatusCollateral { get; set; }
    public decimal? ValueCollateral { get; set; }
    public string? AddressCollateral { get; set; }
    public string? Relationship { get; set; }
    public string? PropertyType { get; set; }
}

public class CollateralConfiguration : IEntityTypeConfiguration<Collateral>
{
    public void Configure(EntityTypeBuilder<Collateral> builder)
    {
        builder.ToTable("Collaterals");

        builder.HasKey(x => x.Ref);

        builder.Property(x => x.Ref)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.StatusCollateral)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.ValueCollateral)
               .IsRequired();

        builder.Property(x => x.AddressCollateral)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(x => x.Relationship)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.PropertyType)
               .IsRequired()
               .HasMaxLength(50);
    }
}