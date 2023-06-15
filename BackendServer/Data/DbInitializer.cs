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
            await _context.SaveChangesAsync();
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
            await _context.SaveChangesAsync();
        }

        if (!_context.InfoCBNVs.Any())
        {
            _context.InfoCBNVs.AddRange(new List<InfoCBNV>()
            {
                new InfoCBNV(){TVTTCode = "001", NameTVTT = "Nguyễn Văn Huy", InfoCBNVBranchCode="001", Branch = _context.Branches.FirstOrDefault(b => b.BranchCode == "001")},
                new InfoCBNV(){TVTTCode = "002", NameTVTT = "Nguyễn Phú Đức", InfoCBNVBranchCode="001", Branch = _context.Branches.FirstOrDefault(b => b.BranchCode == "001")},
                new InfoCBNV(){TVTTCode = "003", NameTVTT = "Đỗ Phát Đạt", InfoCBNVBranchCode="002", Branch = _context.Branches.FirstOrDefault(b => b.BranchCode == "001")},
                new InfoCBNV(){TVTTCode = "004", NameTVTT = "Ngô Trí Trường", InfoCBNVBranchCode="002", Branch = _context.Branches.FirstOrDefault(b => b.BranchCode == "001")},
                new InfoCBNV(){TVTTCode = "005", NameTVTT = "Hoàng Bảo Phúc", InfoCBNVBranchCode="003", Branch = _context.Branches.FirstOrDefault(b => b.BranchCode == "001")}
            });
            await _context.SaveChangesAsync();
        }

        if (!_context.Customers.Any())
        {
            _context.Customers.AddRange(new List<Customer>()
            {
                new Customer(){Cif = "987654321", Name = "Customer1", CustomerType=CustomerType.CustomerType1, Gender = "Nam", CCCD="125087654321"},
                new Customer(){Cif = "987654322", Name = "Customer2", CustomerType=CustomerType.CustomerType1, Gender = "Nam", CCCD="125087654322"},
                new Customer(){Cif = "987654323", Name = "Customer3", CustomerType=CustomerType.CustomerType2, Gender = "Nam", CCCD="125087654323"},
                new Customer(){Cif = "987654324", Name = "Customer4", CustomerType=CustomerType.CustomerType2, Gender = "Nam", CCCD="125087654324"},
            });
            await _context.SaveChangesAsync();
        }

        if (!_context.Collaterals.Any())
        {
            _context.Collaterals.AddRange(new List<Collateral>()
            {
                new Collateral(){Ref = "987654321", StatusCollateral = "Active", ValueCollateral=100000000, AddressCollateral = "ABC", Relationship=Relationship.Relationship1, PropertyType="Nhà phố"},
                new Collateral(){Ref = "987654322", StatusCollateral = "Active", ValueCollateral=100000000, AddressCollateral = "DEF", Relationship=Relationship.Relationship2, PropertyType="Xe"},
                new Collateral(){Ref = "987654323", StatusCollateral = "Blank", ValueCollateral=100000000, AddressCollateral = "DEF2", Relationship=Relationship.Relationship2, PropertyType="Xe"},
                new Collateral(){Ref = "987654324", StatusCollateral = "Active", ValueCollateral=100000000, AddressCollateral = "DEF3", Relationship=Relationship.Relationship2, PropertyType="Xe"},
                new Collateral(){Ref = "987654325", StatusCollateral = "Blank", ValueCollateral=100000000, AddressCollateral = "ABC2", Relationship=Relationship.Relationship2, PropertyType="Nhà phố"},
                new Collateral(){Ref = "987654326", StatusCollateral = "Active", ValueCollateral=100000000, AddressCollateral = "ABC", Relationship=Relationship.Relationship1, PropertyType="Nhà phố"},
                new Collateral(){Ref = "987654327", StatusCollateral = "Active", ValueCollateral=100000000, AddressCollateral = "DEF", Relationship=Relationship.Relationship2, PropertyType="Xe"},
                new Collateral(){Ref = "987654328", StatusCollateral = "Blank", ValueCollateral=100000000, AddressCollateral = "DEF2", Relationship=Relationship.Relationship2, PropertyType="Xe"},
                new Collateral(){Ref = "987654329", StatusCollateral = "Active", ValueCollateral=100000000, AddressCollateral = "DEF3", Relationship=Relationship.Relationship2, PropertyType="Xe"},
            });
            await _context.SaveChangesAsync();
        }

        if (!_context.InsuranceContracts.Any())
        {
            _context.InsuranceContracts.AddRange(new List<InsuranceContract>()
            {
                new InsuranceContract(){
                    HDBH = "987654321",
                    NewOrRenewed = true,
                    STBH=100000000,
                    InsuranceFee = 1000000000,
                    NumberOfPayments=1,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Exception = "",
                    Beneficiaries="Blank",
                    InsuranceType= InsuranceType.InsuranceType1,
                    OtherInsuranceType= "Khác",
                    InsuranceBeneficiary= InsuranceBeneficiary.InsuranceType1,
                    Status = Insuranceapprove.DontSeedapproval.ToString(),
                    Cif = "987654321",
                    TVTTCode = "001",
                    InsurancePartnerCode="001",
                    Customer = _context.Customers.FirstOrDefault(b => b.Cif == "987654321"),
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    Partner = _context.Partners.FirstOrDefault(b => b.PartnerCode == "001"),
                    Collateral = _context.Collaterals.FirstOrDefault(b => b.Ref == "987654321"),
                },

                new InsuranceContract(){
                    HDBH = "987654322",
                    NewOrRenewed = true,
                    STBH=100000000,
                    InsuranceFee = 1000000000,
                    NumberOfPayments=1,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Exception = "",
                    Beneficiaries="Blank",
                    InsuranceType= InsuranceType.InsuranceType2,
                    OtherInsuranceType= "Khác",
                    InsuranceBeneficiary= InsuranceBeneficiary.InsuranceType1,
                    Status = Insuranceapprove.DontSeedapproval.ToString(),
                    Cif = "987654321",
                    TVTTCode = "001",
                    InsurancePartnerCode="001",
                    Customer = _context.Customers.FirstOrDefault(b => b.Cif == "987654321"),
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    Partner = _context.Partners.FirstOrDefault(b => b.PartnerCode == "001"),
                    Collateral = _context.Collaterals.FirstOrDefault(b => b.Ref == "987654322"),
                },

                new InsuranceContract()
                {
                    HDBH = "987654323",
                    NewOrRenewed = true,
                    STBH=100000000,
                    InsuranceFee = 1000000000,
                    NumberOfPayments=1,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Exception = "",
                    Beneficiaries="Blank",
                    InsuranceType= InsuranceType.InsuranceType2,
                    OtherInsuranceType= "Khác",
                    InsuranceBeneficiary= InsuranceBeneficiary.InsuranceType1,
                    Status = Insuranceapprove.DontSeedapproval.ToString(),
                    Cif = "987654321",
                    TVTTCode = "001",
                    InsurancePartnerCode="001",
                    Customer = _context.Customers.FirstOrDefault(b => b.Cif == "987654321"),
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    Partner = _context.Partners.FirstOrDefault(b => b.PartnerCode == "001"),
                    Collateral = _context.Collaterals.FirstOrDefault(b => b.Ref == "987654323"),
                },
                new InsuranceContract(){
                    HDBH = "987654324",
                    NewOrRenewed = true,
                    STBH=100000000,
                    InsuranceFee = 1000000000,
                    NumberOfPayments=1,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Exception = "",
                    Beneficiaries="Blank",
                    InsuranceType= InsuranceType.InsuranceType1,
                    OtherInsuranceType= "Khác",
                    InsuranceBeneficiary= InsuranceBeneficiary.InsuranceType1,
                    Status = Insuranceapprove.DontSeedapproval.ToString(),
                    Cif = "987654321",
                    TVTTCode = "001",
                    InsurancePartnerCode="001",
                    Customer = _context.Customers.FirstOrDefault(b => b.Cif == "987654321"),
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    Partner = _context.Partners.FirstOrDefault(b => b.PartnerCode == "001"),
                    Collateral = _context.Collaterals.FirstOrDefault(b => b.Ref == "987654324"),
                },

                new InsuranceContract(){
                    HDBH = "987654325",
                    NewOrRenewed = true,
                    STBH=100000000,
                    InsuranceFee = 1000000000,
                    NumberOfPayments=1,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Exception = "",
                    Beneficiaries="Blank",
                    InsuranceType= InsuranceType.InsuranceType2,
                    OtherInsuranceType= "Khác",
                    InsuranceBeneficiary= InsuranceBeneficiary.InsuranceType1,
                    Status = Insuranceapprove.DontSeedapproval.ToString(),
                    Cif = "987654321",
                    TVTTCode = "001",
                    InsurancePartnerCode="001",
                    Customer = _context.Customers.FirstOrDefault(b => b.Cif == "987654321"),
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    Partner = _context.Partners.FirstOrDefault(b => b.PartnerCode == "001"),
                    Collateral = _context.Collaterals.FirstOrDefault(b => b.Ref == "987654325"),
                },

                new InsuranceContract()
                {
                    HDBH = "987654326",
                    NewOrRenewed = true,
                    STBH=100000000,
                    InsuranceFee = 1000000000,
                    NumberOfPayments=1,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Exception = "",
                    Beneficiaries="Blank",
                    InsuranceType= InsuranceType.InsuranceType2,
                    OtherInsuranceType= "Khác",
                    InsuranceBeneficiary= InsuranceBeneficiary.InsuranceType1,
                    Status = Insuranceapprove.DontSeedapproval.ToString(),
                    Cif = "987654321",
                    TVTTCode = "001",
                    InsurancePartnerCode="001",
                    Customer = _context.Customers.FirstOrDefault(b => b.Cif == "987654321"),
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    Partner = _context.Partners.FirstOrDefault(b => b.PartnerCode == "001"),
                    Collateral = _context.Collaterals.FirstOrDefault(b => b.Ref == "987654326"),
                },
                new InsuranceContract(){
                    HDBH = "987654327",
                    NewOrRenewed = true,
                    STBH=100000000,
                    InsuranceFee = 1000000000,
                    NumberOfPayments=1,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Exception = "",
                    Beneficiaries="Blank",
                    InsuranceType= InsuranceType.InsuranceType1,
                    OtherInsuranceType= "Khác",
                    InsuranceBeneficiary= InsuranceBeneficiary.InsuranceType1,
                    Status = Insuranceapprove.DontSeedapproval.ToString(),
                    Cif = "987654321",
                    TVTTCode = "001",
                    InsurancePartnerCode="001",
                    Customer = _context.Customers.FirstOrDefault(b => b.Cif == "987654321"),
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    Partner = _context.Partners.FirstOrDefault(b => b.PartnerCode == "001"),
                    Collateral = _context.Collaterals.FirstOrDefault(b => b.Ref == "987654327"),
                },

                new InsuranceContract(){
                    HDBH = "987654328",
                    NewOrRenewed = true,
                    STBH=100000000,
                    InsuranceFee = 1000000000,
                    NumberOfPayments=1,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Exception = "",
                    Beneficiaries="Blank",
                    InsuranceType= InsuranceType.InsuranceType2,
                    OtherInsuranceType= "Khác",
                    InsuranceBeneficiary= InsuranceBeneficiary.InsuranceType1,
                    Status = Insuranceapprove.DontSeedapproval.ToString(),
                    Cif = "987654321",
                    TVTTCode = "001",
                    InsurancePartnerCode="001",
                    Customer = _context.Customers.FirstOrDefault(b => b.Cif == "987654321"),
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    Partner = _context.Partners.FirstOrDefault(b => b.PartnerCode == "001"),
                    Collateral = _context.Collaterals.FirstOrDefault(b => b.Ref == "987654328"),
                },

                new InsuranceContract()
                {
                    HDBH = "987654329",
                    NewOrRenewed = true,
                    STBH=100000000,
                    InsuranceFee = 1000000000,
                    NumberOfPayments=1,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Exception = "",
                    Beneficiaries="Blank",
                    InsuranceType= InsuranceType.InsuranceType2,
                    OtherInsuranceType= "Khác",
                    InsuranceBeneficiary= InsuranceBeneficiary.InsuranceType1,
                    Status = Insuranceapprove.DontSeedapproval.ToString(),
                    Cif = "987654321",
                    TVTTCode = "001",
                    InsurancePartnerCode="001",
                    Customer = _context.Customers.FirstOrDefault(b => b.Cif == "987654321"),
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    Partner = _context.Partners.FirstOrDefault(b => b.PartnerCode == "001"),
                    Collateral = _context.Collaterals.FirstOrDefault(b => b.Ref == "987654329"),
                }
            });
            await _context.SaveChangesAsync();
        }

        if (!_context.AnnexContracts.Any())
        {
            _context.AnnexContracts.AddRange(new List<AnnexContract>()
            {
                new AnnexContract()
                {
                    HDPL = "987654321",
                    AnnexPerson = "Nguyễn Văn Huy",
                    AdditionalAnnexFee=100000000,
                    AnnexFeeVAT=10000000,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Beneficiaries = "001-Tân Bình",
                    Status =Insuranceapprove.DontSeedapproval.ToString(),
                    TVTTCode = "001",
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    HDBH = "987654321",
                    InsuranceContract = _context.InsuranceContracts.FirstOrDefault(b => b.HDBH == "987654321"),
                },
                new AnnexContract()
                {
                    HDPL = "987654322",
                    AnnexPerson = "Nguyễn Văn Huy",
                    AdditionalAnnexFee=100000000,
                    AnnexFeeVAT=10000000,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Beneficiaries = "001-Tân Bình",
                    Status = Insuranceapprove.DontSeedapproval.ToString(),
                    TVTTCode = "001",
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    HDBH = "987654322",
                    InsuranceContract = _context.InsuranceContracts.FirstOrDefault(b => b.HDBH == "987654322"),
                },
                new AnnexContract()
                {
                    HDPL = "987654323",
                    AnnexPerson = "Nguyễn Văn Huy",
                    AdditionalAnnexFee=100000000,
                    AnnexFeeVAT=10000000,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Beneficiaries = "001-Tân Bình",
                    Status = Insuranceapprove.DontSeedapproval.ToString(),
                    TVTTCode = "001",
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    HDBH = "987654323",
                    InsuranceContract = _context.InsuranceContracts.FirstOrDefault(b => b.HDBH == "987654323"),
                },
                new AnnexContract()
                {
                    HDPL = "987654324",
                    AnnexPerson = "Nguyễn Văn Huy",
                    AdditionalAnnexFee=100000000,
                    AnnexFeeVAT=10000000,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Beneficiaries = "001-Tân Bình",
                    Status = Insuranceapprove.DontSeedapproval.ToString(),
                    TVTTCode = "001",
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    HDBH = "987654321",
                    InsuranceContract = _context.InsuranceContracts.FirstOrDefault(b => b.HDBH == "987654324"),
                },
                new AnnexContract()
                {
                    HDPL = "987654325",
                    AnnexPerson = "Nguyễn Văn Huy",
                    AdditionalAnnexFee=100000000,
                    AnnexFeeVAT=10000000,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Beneficiaries = "001-Tân Bình",
                    Status =Insuranceapprove.DontSeedapproval.ToString(),
                    TVTTCode = "001",
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    HDBH = "987654322",
                    InsuranceContract = _context.InsuranceContracts.FirstOrDefault(b => b.HDBH == "987654325"),
                },
                new AnnexContract()
                {
                    HDPL = "987654326",
                    AnnexPerson = "Nguyễn Văn Huy",
                    AdditionalAnnexFee=100000000,
                    AnnexFeeVAT=10000000,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Beneficiaries = "001-Tân Bình",
                    Status = Insuranceapprove.DontSeedapproval.ToString(),
                    TVTTCode = "001",
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    HDBH = "987654323",
                    InsuranceContract = _context.InsuranceContracts.FirstOrDefault(b => b.HDBH == "987654326"),
                },
                new AnnexContract()
                {
                    HDPL = "987654327",
                    AnnexPerson = "Nguyễn Văn Huy",
                    AdditionalAnnexFee=100000000,
                    AnnexFeeVAT=10000000,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Beneficiaries = "001-Tân Bình",
                    Status = Insuranceapprove.DontSeedapproval.ToString(),
                    TVTTCode = "001",
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    HDBH = "987654321",
                    InsuranceContract = _context.InsuranceContracts.FirstOrDefault(b => b.HDBH == "987654327"),
                },
                new AnnexContract()
                {
                    HDPL = "987654328",
                    AnnexPerson = "Nguyễn Văn Huy",
                    AdditionalAnnexFee=100000000,
                    AnnexFeeVAT=10000000,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Beneficiaries = "001-Tân Bình",
                    Status =Insuranceapprove.DontSeedapproval.ToString(),
                    TVTTCode = "001",
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    HDBH = "987654322",
                    InsuranceContract = _context.InsuranceContracts.FirstOrDefault(b => b.HDBH == "987654328"),
                },
                new AnnexContract()
                {
                    HDPL = "987654329",
                    AnnexPerson = "Nguyễn Văn Huy",
                    AdditionalAnnexFee=100000000,
                    AnnexFeeVAT=10000000,
                    FromDate=DateTime.UtcNow.AddDays(-100),
                    ToDate=DateTime.UtcNow.AddDays(-10),
                    Beneficiaries = "001-Tân Bình",
                    Status = Insuranceapprove.DontSeedapproval.ToString(),
                    TVTTCode = "001",
                    InfoCBNV = _context.InfoCBNVs.FirstOrDefault(b => b.TVTTCode == "001"),
                    HDBH = "987654323",
                    InsuranceContract = _context.InsuranceContracts.FirstOrDefault(b => b.HDBH == "987654329"),
                }
            }) ;
            await _context.SaveChangesAsync();
        }

        await _context.SaveChangesAsync();
    }
}