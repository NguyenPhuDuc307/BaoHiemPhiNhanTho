namespace BackendServer.Models.AnnexContractViewModel
{
    public class AnnexContractNewRequest
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
        public string? HDBH { get; set; }    }
}