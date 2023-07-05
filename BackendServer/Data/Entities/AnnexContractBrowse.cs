using BackendServer.Data.Entities;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BackendServer.Data.Entities
{
    public class AnnexContractBrowse
    {
        public int id { get; set; }
        public string message { get; set; }
        public DateTime timeSpace { get; set; }

        // n-n
        public string? HDPL { get; set; }

        public AnnexContract annexContract { get; set; }

        // n-n
        public int browserInformationId { get; set; }

        public BrowserInformation BrowserInformation { get; set; }
    }
}

public class AnnexContractBrowseConfiguration : IEntityTypeConfiguration<AnnexContractBrowse>
{
    public void Configure(EntityTypeBuilder<AnnexContractBrowse> builder)
    {
        builder.ToTable("AnnexContractBrowses");
        builder.HasKey(x => x.id);
        builder.Property(x => x.id)
               .IsRequired().UseIdentityColumn();
        builder.Property(x => x.message)
               .IsRequired();
    }
}