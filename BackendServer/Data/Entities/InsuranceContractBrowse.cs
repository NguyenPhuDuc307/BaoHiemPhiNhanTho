using BackendServer.Data.Entities;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BackendServer.Data.Entities
{
    public class InsuranceContractBrowse
    {
        public int id {  get; set; }
        public string message { get; set; }
        public DateTime timeSpace { get; set; }
        // n-n
        public string? HDBH { get; set; }
        public InsuranceContract InsuranceContract { get; set; }
        // n-n
        public int browserInformationId { get; set; }
        public BrowserInformation BrowserInformation { get; set; }
    }
}
public class InsuranceContractBrowseConfiguration : IEntityTypeConfiguration<InsuranceContractBrowse>
{
    public void Configure(EntityTypeBuilder<InsuranceContractBrowse> builder)
    {
        builder.ToTable("InsuranceContractBrowses");
        builder.HasKey(x => x.id);
        builder.Property(x => x.id)
               .IsRequired().UseIdentityColumn();
        builder.Property(x => x.message)
               .IsRequired();
    }
}