using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaoHiemPhiNhanTho.BackendServer.Models;

public class AnnexContract
{
    public string? HDPL { get; set; }
    public bool? NewOrRenewed { get; set; }
    public decimal? STBH { get; set; }
    public decimal? InsuranceFee { get; set; }
    public string? NumberOfPayments { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? Exception { get; set; }
    public string? HDBH { get; set; }
    public InsuranceContract? InsuranceContract { set; get; }
    public string? TVTTCode { get; set; }
    public InfoCBNV? InfoCBNV { set; get; }
    public string? Status { get; set; }
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

        builder.Property(x => x.NewOrRenewed)
               .IsRequired();

        builder.Property(x => x.STBH)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(x => x.InsuranceFee)
               .IsRequired();

        builder.Property(x => x.FromDate)
               .IsRequired();

        builder.Property(x => x.ToDate)
               .IsRequired();

        builder.Property(x => x.Exception)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(x => x.Status)
               .IsRequired();

        builder.HasOne(x => x.InsuranceContract)
                   .WithMany(x => x.AnnexContracts)
                   .HasForeignKey(x => x.HDBH);

        builder.HasOne(x => x.InfoCBNV)
                   .WithMany(x => x.AnnexContracts)
                   .HasForeignKey(x => x.TVTTCode);
    }
}