﻿using BackendServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaoHiemPhiNhanTho.BackendServer.Models;

public class InsuranceContract
{
    public string? HDBH { get; set; }
    public bool? NewOrRenewed { get; set; }
    public decimal? STBH { get; set; }
    public decimal? InsuranceFee { get; set; }
    public int? NumberOfPayments { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? Exception { get; set; }
    public string? Beneficiaries { get; set; }
    public string? InsuranceType { get; set; }
    public string? OtherInsuranceType { get; set; }
    public string? InsuranceBeneficiary { get; set; }
    public string? Status { get; set; }
    public string? Cif { get; set; }
    public Customer? Customer { set; get; }
    public string? TVTTCode { get; set; }
    public InfoCBNV? InfoCBNV { set; get; }
    public string? InsurancePartnerCode { get; set; }
    public Partner? Partner { set; get; }
    public string? Ref { get; set; }
    public Collateral? Collateral { set; get; }
    public string? HDPL { set; get; }
    public AnnexContract? AnnexContract { set; get; }
    public ICollection<PaymentPeriod>? PaymentPeriods { set; get; }

    public static explicit operator string(InsuranceContract v)
    {
        throw new NotImplementedException();
    }
}

public class InsuranceContractConfiguration : IEntityTypeConfiguration<InsuranceContract>
{
    public void Configure(EntityTypeBuilder<InsuranceContract> builder)
    {
        builder.ToTable("InsuranceContracts");

        builder.HasKey(x => x.HDBH);

        builder.Property(x => x.HDBH)
               .IsRequired();

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

        builder.Property(x => x.InsurancePartnerCode)
              .IsRequired();

        builder.Property(x => x.Exception)
               .IsRequired();

        builder.Property(x => x.Beneficiaries)
               .IsRequired();

        builder.Property(x => x.InsuranceType)
               .IsRequired();

        builder.Property(x => x.OtherInsuranceType)
              .IsRequired();

        builder.Property(x => x.Status)
              .IsRequired();

        builder.Property(x => x.InsuranceBeneficiary)
               .IsRequired();

        builder.Property(x => x.Cif)
        .IsRequired();

        builder.Property(x => x.TVTTCode)
       .IsRequired();

        builder.Property(x => x.Ref)
        .IsRequired();

        builder.Property(x => x.HDPL)
        .IsRequired();

        builder.HasOne(x => x.Customer)
              .WithMany(x => x.InsuranceContracts)
              .HasForeignKey(x => x.Cif);

        builder.HasOne(x => x.InfoCBNV)
              .WithMany(x => x.InsuranceContracts)
              .HasForeignKey(x => x.TVTTCode);

        builder.HasOne(x => x.Partner)
              .WithMany(x => x.InsuranceContracts)
              .HasForeignKey(x => x.InsurancePartnerCode);
    }
}