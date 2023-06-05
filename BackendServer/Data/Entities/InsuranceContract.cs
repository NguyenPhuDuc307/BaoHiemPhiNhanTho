﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaoHiemPhiNhanTho.BackendServer.Models;

public class InsuranceContract
{
    public string? HDBH { get; set; }
    public string? NewOrRenewed { get; set; }
    public float? STBH { get; set; }
    public float? InsuranceFee { get; set; }
    public int? NumberOfPayments { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? Exception { get; set; }
    public string? Beneficiaries { get; set; }
    public string? InsuranceType { get; set; }
    public string? Cif { get; set; }
    public Customer? Customer { set; get; }
    public string? TVTTCode { get; set; }
    public InfoCBNV? InfoCBNV { set; get; }
    public ICollection<AnnexContract>? AnnexContracts{ set; get; }
    public ICollection<PaymentPeriod>? PaymentPeriods{ set; get; }

}

public class InsuranceContractConfiguration : IEntityTypeConfiguration<InsuranceContract>
{
    public void Configure(EntityTypeBuilder<InsuranceContract> builder)
    {
        builder.ToTable("InsuranceContracts");

        builder.HasKey(x => x.HDBH);

        builder.Property(x => x.HDBH)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.NewOrRenewed)
               .IsRequired();

        builder.Property(x => x.STBH)
               .IsRequired();

        builder.Property(x => x.InsuranceFee)
               .IsRequired();

        builder.Property(x => x.NumberOfPayments)
               .IsRequired();

        builder.Property(x => x.FromDate)
               .IsRequired();

        builder.Property(x => x.ToDate)
               .IsRequired();

        builder.Property(x => x.Exception)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(x => x.Beneficiaries)
               .IsRequired()
               .HasMaxLength(50);

        builder.HasOne(x => x.Customer)
                   .WithMany(x => x.InsuranceContracts)
                   .HasForeignKey(x => x.Cif);

        builder.HasOne(x => x.InfoCBNV)
                   .WithMany(x => x.InsuranceContracts)
                   .HasForeignKey(x => x.TVTTCode);
    }
}