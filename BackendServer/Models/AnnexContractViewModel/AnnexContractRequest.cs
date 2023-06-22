using BaoHiemPhiNhanTho.BackendServer.Models;

namespace BackendServer.Models.AnnexContractViewModel
{
    public class AnnexContractRequest
    {
        public string? HDPL { get; set; }
        public string? AnnexPerson { get; set; }
        public decimal? AdditionalAnnexFee { get; set; }
        public decimal? AnnexFeeVAT { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? Beneficiaries { get; set; }
        public string? Status { get; set; }
        public string? TVTTCode { get; set; }
        public string? NameTVTT { get; set; }
        public string? BranchName { get; set; }
        public string? AnnexBeneficiary { get; set; }
        public string? HDBH { get; set; }
        public string? InsuranceType { get; set; }
    }
}