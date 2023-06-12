using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaoHiemPhiNhanTho.BackendServer.Models;

public class AnnexContract
{
    public string? HDPL { get; set; }
    public string? AnnexPerson { get; set; }
    public decimal? AdditionalAnnexFee { get; set; }
    public decimal? AnnexFeeVAT { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? Beneficiaries { get; set; }
    public string? Status { get; set; }
    public string? TVTTCode { get; set; }
    public InfoCBNV? InfoCBNV { set; get; }
    public string? HDBH { get; set; }
    public InsuranceContract? InsuranceContract { set; get; }
}

public class AnnexContractConfiguration : IEntityTypeConfiguration<AnnexContract>
{
    public void Configure(EntityTypeBuilder<AnnexContract> builder)
    {
        builder.ToTable("AnnexContracts");

        builder.HasKey(x => x.HDPL);

        builder.Property(x => x.HDPL)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.AdditionalAnnexFee)
               .IsRequired();

        builder.Property(x => x.AnnexFeeVAT)
               .IsRequired();

        builder.Property(x => x.FromDate)
               .IsRequired();

        builder.Property(x => x.ToDate)
               .IsRequired();

        builder.Property(x => x.TVTTCode)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.HDBH)
              .IsRequired()
              .HasMaxLength(50);

        builder.Property(x => x.Beneficiaries)
              .IsRequired()
              .HasMaxLength(255);

        builder.Property(x => x.Status)
               .IsRequired();

        builder.HasOne(x => x.InfoCBNV)
               .WithMany(x => x.AnnexContracts)
               .HasForeignKey(x => x.TVTTCode);
    }
}