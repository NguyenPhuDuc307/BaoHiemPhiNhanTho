using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaoHiemPhiNhanTho.BackendServer.Models;

public class PaymentPeriod
{
    public int? Id { get; set; }
    public DateTime? FeePaymentDate { get; set; }
    public decimal? Money { get; set; }
    public string? HDBH { get; set; }
    public InsuranceContract? InsuranceContract { set; get; }
}

public class PaymentPeriodConfiguration : IEntityTypeConfiguration<PaymentPeriod>
{
    public void Configure(EntityTypeBuilder<PaymentPeriod> builder)
    {
        builder.ToTable("PaymentPeriods");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .IsRequired().UseIdentityColumn();

        builder.Property(x => x.FeePaymentDate)
               .IsRequired();

        builder.Property(x => x.Money)
               .IsRequired().HasColumnType("decimal(18,2)");

        builder.Property(x => x.HDBH)
       .IsRequired()
       .HasMaxLength(50);

        builder.HasOne(x => x.InsuranceContract)
              .WithMany(x => x.PaymentPeriods)
              .HasForeignKey(x => x.HDBH);
    }
}