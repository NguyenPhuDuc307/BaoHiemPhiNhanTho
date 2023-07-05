using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaoHiemPhiNhanTho.BackendServer.Models;

public class Branch
{
    public string? BranchCode { get; set; }
    public string? BranchName { get; set; }
    public ICollection<InfoCBNV>? InfoCBNVs { set; get; }
}

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(x => x.BranchCode);

        builder.Property(x => x.BranchCode)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.BranchName)
               .IsRequired()
               .HasMaxLength(255);
    }
}