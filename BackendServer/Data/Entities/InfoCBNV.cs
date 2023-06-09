using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaoHiemPhiNhanTho.BackendServer.Models;

public class InfoCBNV
{
    public string? TVTTCode { get; set; }
    public string? NameTVTT { get; set; }
    public ICollection<InsuranceContract>? InsuranceContracts { set; get; }
    public ICollection<AnnexContract>? AnnexContracts { set; get; }
    public string? InfoCBNVBranchCode { get; set; }

    public Branch? Branch { set; get; }
}

public class InfoCBNVConfiguration : IEntityTypeConfiguration<InfoCBNV>
{
    public void Configure(EntityTypeBuilder<InfoCBNV> builder)
    {
        builder.ToTable("InfoCBNVs");

        builder.HasKey(x => x.TVTTCode);

        builder.Property(x => x.TVTTCode)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.NameTVTT)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(x => x.InfoCBNVBranchCode)
               .IsRequired()
               .HasMaxLength(50);

        builder.HasOne(x => x.Branch)
              .WithMany(x => x.InfoCBNVs)
              .HasForeignKey(x => x.InfoCBNVBranchCode);
    }
}
