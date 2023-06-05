using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaoHiemPhiNhanTho.BackendServer.Models;

public class Partner
{
    public string? PartnersCode { get; set; }
    public string? Name { get; set; }
}

public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
{
    public void Configure(EntityTypeBuilder<Partner> builder)
    {
        builder.ToTable("Partners");

        builder.HasKey(x => x.PartnersCode);

        builder.Property(x => x.PartnersCode)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(255);

    }
}