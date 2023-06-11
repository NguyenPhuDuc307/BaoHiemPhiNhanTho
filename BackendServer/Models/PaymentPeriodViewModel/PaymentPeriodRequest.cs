namespace BackendServer.Models.PaymentPeriodViewModel
{
    public class PaymentPeriodRequest
    {
        public DateTime? FeePaymentDate { get; set; }
        public decimal? Money { get; set; }
        public string? HDBH { get; set; }
    }
}