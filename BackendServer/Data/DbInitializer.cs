using BackendServer.Data.EF;
using BackendServer.Data.Enums;
using BaoHiemPhiNhanTho.BackendServer.Models;

namespace BackendServer.Data;

public class DbInitializer
{
    private readonly BHPNTDbContext _context;

    public DbInitializer(BHPNTDbContext context)
    {
        _context = context;
    }

    public async Task Seed()
    {
        if (!_context.Partners.Any())
        {
            _context.Partners.AddRange(new List<Partner>()
            {
                new Partner(){PartnerCode = "001", Name = "ABC"},
                new Partner(){PartnerCode = "002", Name = "DEF"},
                new Partner(){PartnerCode = "003", Name = "XYZ"},
                new Partner(){PartnerCode = "004", Name = "MNO"},
                new Partner(){PartnerCode = "005", Name = "QPR"}
            });
        }

        if (!_context.Branches.Any())
        {
            _context.Branches.AddRange(new List<Branch>()
            {
                new Branch(){BranchCode = "001", BranchName = "CN Tân Bình"},
                new Branch(){BranchCode = "002", BranchName = "CN Gò Vấp"},
                new Branch(){BranchCode = "003", BranchName = "CN Bình Thạnh"},
                new Branch(){BranchCode = "004", BranchName = "CN Quận 1"},
                new Branch(){BranchCode = "005", BranchName = "CN Quận 2"}
            });
        }

        if (!_context.InfoCBNVs.Any())
        {
            _context.InfoCBNVs.AddRange(new List<InfoCBNV>()
            {
                new InfoCBNV(){TVTTCode = "001", NameTVTT = "Nguyễn Văn Huy", BranchCode="001"},
                new InfoCBNV(){TVTTCode = "002", NameTVTT = "Nguyễn Phú Đức", BranchCode="001"},
                new InfoCBNV(){TVTTCode = "003", NameTVTT = "Đỗ Phát Đạt", BranchCode="002"},
                new InfoCBNV(){TVTTCode = "004", NameTVTT = "Ngô Trí Trường", BranchCode="002"},
                new InfoCBNV(){TVTTCode = "005", NameTVTT = "Hoàng Bảo Phúc", BranchCode="003"}
            });
        }

        if (!_context.Customers.Any())
        {
            _context.Customers.AddRange(new List<Customer>()
            {
                new Customer(){Cif = "987654321", Name = "Customer1", CustomerType=CustomerType.KHDN, Gender = "Nam", CCCD="125087654321"},
                new Customer(){Cif = "987654322", Name = "Customer2", CustomerType=CustomerType.KHDN, Gender = "Nam", CCCD="125087654322"},
                new Customer(){Cif = "987654323", Name = "Customer3", CustomerType=CustomerType.KHCN, Gender = "Nam", CCCD="125087654323"},
                new Customer(){Cif = "987654324", Name = "Customer4", CustomerType=CustomerType.KHCN, Gender = "Nam", CCCD="125087654324"},

            });
        }

        if (!_context.Collaterals.Any())
        {
            _context.Collaterals.AddRange(new List<Collateral>()
            {
                new Collateral(){Ref = "987654321", StatusCollateral = "Active", ValueCollateral=100000000, AddressCollateral = "ABC", Relationship="Chủ sở hữu", PropertyType="Nhà phố"},
                new Collateral(){Ref = "987654322", StatusCollateral = "Active", ValueCollateral=100000000, AddressCollateral = "DEF", Relationship="Chủ sở hữu", PropertyType="Xe"},
                new Collateral(){Ref = "987654323", StatusCollateral = "Blank", ValueCollateral=100000000, AddressCollateral = "DEF2", Relationship="Chủ sở hữu", PropertyType="Xe"},
                new Collateral(){Ref = "987654324", StatusCollateral = "Active", ValueCollateral=100000000, AddressCollateral = "DEF3", Relationship="Chủ sở hữu", PropertyType="Xe"},
                new Collateral(){Ref = "987654325", StatusCollateral = "Blank", ValueCollateral=100000000, AddressCollateral = "ABC2", Relationship="Chủ sở hữu", PropertyType="Nhà phố"},
            });
        }

        if (!_context.InsuranceContracts.Any())
        {
            _context.InsuranceContracts.AddRange(new List<InsuranceContract>()
            {
                new InsuranceContract(){HDBH = "987654321",
                NewOrRenewed = "New",
                STBH=100000000,
                InsuranceFee = 1000000000,
                NumberOfPayments=1,
                FromDate=DateTime.UtcNow.AddDays(-100),
                ToDate=DateTime.UtcNow.AddDays(-10),
                Exception = "",
                Beneficiaries="Blank",
                InsuranceType= "Bảo hảnh nhà tư nhân",
                Cif = "987654321",
                TVTTCode = "001",
                PartnerCode="001",
                CollateralRef = "987654321"},

                new InsuranceContract(){HDBH = "987654322",
                NewOrRenewed = "New",
                STBH=100000000,
                InsuranceFee = 1000000000,
                NumberOfPayments=1,
                FromDate=DateTime.UtcNow.AddDays(-100),
                ToDate=DateTime.UtcNow.AddDays(-10),
                Exception = "",
                Beneficiaries="Blank",
                InsuranceType= "Bảo hảnh nhà tư nhân",
                Cif = "987654321",
                TVTTCode = "001",
                PartnerCode="001",
                CollateralRef = "987654321"},

                new InsuranceContract(){HDBH = "987654323",
                NewOrRenewed = "New",
                STBH=100000000,
                InsuranceFee = 1000000000,
                NumberOfPayments=1,
                FromDate=DateTime.UtcNow.AddDays(-100),
                ToDate=DateTime.UtcNow.AddDays(-10),
                Exception = "",
                Beneficiaries="Blank",
                InsuranceType= "Bảo hảnh nhà tư nhân",
                Cif = "987654321",
                TVTTCode = "001",
                PartnerCode="001",
                CollateralRef = "987654321"},

            });
        }

        if (!_context.AnnexContracts.Any())
        {
            _context.AnnexContracts.AddRange(new List<AnnexContract>()
            {
                new AnnexContract(){HDPL = "987654321",
                NewOrRenewed = true, STBH=100000000,
                InsuranceFee=100000000,
                NumberOfPayments=1,
                FromDate=DateTime.UtcNow.AddDays(-100),
                ToDate=DateTime.UtcNow.AddDays(-10),
                Exception = "",
                HDBH = "987654321",TVTTCode = "001"},

                new AnnexContract(){HDPL = "987654322",
                NewOrRenewed = true, STBH=100000000,
                InsuranceFee=100000000,
                NumberOfPayments=1,
                FromDate=DateTime.UtcNow.AddDays(-100),
                ToDate=DateTime.UtcNow.AddDays(-10),
                Exception = "",
                HDBH = "987654321",
                TVTTCode = "001"},

                new AnnexContract(){HDPL = "987654323",
                NewOrRenewed = true,
                STBH=100000000,
                InsuranceFee=100000000,
                NumberOfPayments=1,
                FromDate=DateTime.UtcNow.AddDays(-100),
                ToDate=DateTime.UtcNow.AddDays(-10),
                Exception = "",
                HDBH = "987654321",
                TVTTCode = "001"}
            });
        }

        await _context.SaveChangesAsync();
    }
}