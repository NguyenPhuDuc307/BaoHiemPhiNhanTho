using BaoHiemPhiNhanTho.BackendServer.Models;

namespace BackendServer.Models.HopDongPhuLucVM
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
        public string? Exception { get; set; }
        public string? Beneficiaries { get; set; }
        public string? InsuranceType { get; set; }
        public string? OtherInsuranceType { get; set; }
        public string? InsuranceBeneficiary { get; set; }
        public string? Cif { get; set; }
        public string? TVTTCode { get; set; }
        public string? PartnerCode { get; set; }
        public string? CollateralRef { get; set; }
        public string? CustomerName { get; set; }

    }
}