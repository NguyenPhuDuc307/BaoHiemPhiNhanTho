using BaoHiemPhiNhanTho.BackendServer.Models;
using BackendServer.Data.Enums;
using BackendServer.Models.PaymentPeriodViewModel;

namespace BackendServer.Models.InsuranceContractViewModel
{
    public class InsuranceContractRequest
    {
        public string? HDBH { get; set; }
        public bool? NewOrRenewed { get; set; }
        public decimal? STBH { get; set; }
        public decimal? InsuranceFee { get; set; }
        public int? NumberOfPayments { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? DateFee { get; set; }
        public string? Exception { get; set; }
        public string? Beneficiaries { get; set; }
        public string? InsuranceType { get; set; }
        public string? OtherInsuranceType { get; set; }
        public string? InsuranceBeneficiary { get; set; }
        public string? Status { get; set; }
        public string? Cif { get; set; }
        public string? TVTTCode { get; set; }
        public string? InsurancePartnerCode { get; set; }
        public string? CollateralRef { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerType { get; set; }
        public string? CCCD { get; set; }
        public string? PartnerName { get; set; }
        public string? StatusCollateral { get; set; }
        public string? CollateralType { get; set; }
        public Decimal? ValueCollateral { get; set; }
        public string? AddressCollateral { get; set; }
        public string? Relationship { get; set; }
        public string? NameTVTT { get; set; }
        public string? BranchName { get; set; }
        public string? HDPL { get; set; }
        public List<PaymentPeriodRequest> lstPaymentPeriod { get; set; }
    }
}