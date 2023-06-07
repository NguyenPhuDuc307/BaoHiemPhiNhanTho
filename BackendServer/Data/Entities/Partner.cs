using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaoHiemPhiNhanTho.BackendServer.Models;

public class Partner
{
    public string? PartnerCode { get; set; }
    public string? Name { get; set; }
    public ICollection<InsuranceContract>? InsuranceContracts { set; get; }
}

public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
{
    public void Configure(EntityTypeBuilder<Partner> builder)
    {
        builder.ToTable("Partners");

        builder.HasKey(x => x.PartnerCode);

        builder.Property(x => x.PartnerCode)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(255);

    }
}