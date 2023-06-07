using BackendServer.Data.EF;
using BackendServer.Models.PaymentPeriodVM;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentPeriodController : ControllerBase
    {
        private readonly BHPNTDbContext _context;

        public PaymentPeriodController(BHPNTDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SavePaymentPeriod(PaymentPeriodVM request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment = new PaymentPeriod()
            {
                TotalAmount = request.TotalAmount,
                Period = request.Period,
                FeePaymentDate = request.FeePaymentDate,
                Money = request.Money,
                HDBH = request.HDBH
            };

            _context.PaymentPeriods.Add(payment);
            await _context.SaveChangesAsync();
            return Ok(payment);
        }
    }
}