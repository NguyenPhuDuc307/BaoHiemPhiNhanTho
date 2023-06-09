﻿using BackendServer.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaoHiemPhiNhanTho.BackendServer.Models;

public class Customer
{
    public string? Cif { get; set; }
    public string? Name { get; set; }
    public string? CustomerType { get; set; }
    public string? Gender { get; set; }
    public string? CCCD { get; set; }
    public ICollection<InsuranceContract>? InsuranceContracts { set; get; }

}

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(x => x.Cif);

        builder.Property(x => x.Cif)
              .IsRequired()
              .HasMaxLength(50);

        builder.Property(x => x.Name)
              .IsRequired()
              .HasMaxLength(255);

        builder.Property(x => x.Gender)
              .IsRequired()
              .HasMaxLength(50);

        builder.Property(x => x.CCCD)
              .IsRequired()
              .HasMaxLength(50);

        builder.Property(x => x.CustomerType)
              .IsRequired()
              .HasMaxLength(50);
    }
}