

using BaoHiemPhiNhanTho.BackendServer.Models;
using BackendServer.Data.Enums;

namespace BackendServer.Models.InsuranceContractViewModel
{
    public class InsuranceContractNewRequest
    {
        public string? HDBH { get; set; }
        public bool? NewOrRenewed { get; set; }
        public decimal? STBH { get; set; }
        public decimal? InsuranceFee { get; set; }
        public int? NumberOfPayments { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
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
    }
}
