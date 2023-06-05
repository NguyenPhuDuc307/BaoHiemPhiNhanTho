using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaoHiemPhiNhanTho.BackendServer.Models;

public class PaymentPeriod
{
    public string? Id { get; set; }
    public string? TotalAmount { get; set; }
    public string? Period { get; set; }
    public DateTime? FeePaymentDate { get; set; }
    public float? Money { get; set; }

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
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.TotalAmount)
               .IsRequired();

        builder.Property(x => x.Period)
               .IsRequired();

        builder.Property(x => x.FeePaymentDate)
               .IsRequired();

        builder.Property(x => x.Money)
               .IsRequired();

        builder.HasOne(x => x.InsuranceContract)
                   .WithMany(x => x.PaymentPeriods)
                   .HasForeignKey(x => x.HDBH);
    }
}