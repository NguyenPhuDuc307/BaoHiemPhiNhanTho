using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BackendServer.Data.Entities;

namespace BackendServer.Data.Entities
{
    public class BrowserInformation
    {
        public string browserInformationId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string position { get; set; }
        public string area { get; set; }
        public string branch { get; set; }
        public IList<InsuranceContractBrowse>? insuranceContractBrowses { set; get; }
        public IList<AnnexContractBrowse>? annexContractBrowses { set; get; }
    }
}

public class BrowserInformationConfiguration : IEntityTypeConfiguration<BrowserInformation>
{
    public void Configure(EntityTypeBuilder<BrowserInformation> builder)
    {
        builder.ToTable("BrowserInformations");
        builder.HasKey(x => x.browserInformationId);
        builder.Property(x => x.browserInformationId)
               .IsRequired();
        builder.Property(x => x.name)
               .IsRequired();
        builder.Property(x => x.email)
               .IsRequired();
        builder.Property(x => x.position)
               .IsRequired();
        builder.Property(x => x.area)
               .IsRequired();
        builder.Property(x => x.branch)
               .IsRequired();
    }
}