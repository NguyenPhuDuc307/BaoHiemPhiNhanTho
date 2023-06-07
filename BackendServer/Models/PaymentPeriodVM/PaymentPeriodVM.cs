using BaoHiemPhiNhanTho.BackendServer.Models;

namespace BackendServer.Models.PaymentPeriodVM
{
    public class PaymentPeriodVM
    {
        public int? TotalAmount { get; set; }
        public string? Period { get; set; }
        public DateTime? FeePaymentDate { get; set; }
        public decimal? Money { get; set; }
        public string? HDBH { get; set; }
    }
}