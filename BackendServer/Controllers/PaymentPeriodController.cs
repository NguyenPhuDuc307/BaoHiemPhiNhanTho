using BackendServer.Data.EF;
using BackendServer.Models.PaymentPeriodViewModel;
using BackendServer.validator.HopDongPhuLuc;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentPeriodController : ControllerBase
    {
        private readonly ILogger<PaymentPeriodController> _logger;
        private readonly BHPNTDbContext _context;

        public PaymentPeriodController(ILogger<PaymentPeriodController> logger, BHPNTDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("CreateSinglePaymentPeriod")]
        [ServiceFilter(typeof(AuthorizeCustomFilter))]
        public async Task<IActionResult> CreatePaymentPeriod([FromBody] PaymentPeriodRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var insurance = await _context.InsuranceContracts.FirstOrDefaultAsync(x => x.HDBH == request.HDBH);

                var payment = new PaymentPeriod()
                {
                    FeePaymentDate = request.FeePaymentDate,
                    Money = request.Money,
                    HDBH = request.HDBH,
                    InsuranceContract = insurance
                };

                _context.PaymentPeriods.Add(payment);
                int result = await _context.SaveChangesAsync();
                if (result <= 0)
                {
                    return BadRequest("Something went wrong, can't add it");
                }
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateMultiPaymentPeriod")]
        public async Task<IActionResult> CreateMultiPaymentPeriods([FromBody] List<PaymentPeriodRequest> requests)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                // validation
                var validator = new PaymentPeriodValidator();
                var validationResult = validator.Validate(requests[0]);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                // tìm hợp đồng thêm kỳ đóng
                decimal? sum = 0;
                var insurance = await _context.InsuranceContracts.FirstOrDefaultAsync(x => x.HDBH == requests[0].HDBH);
                if (insurance == null)
                {
                    return BadRequest("Không tìm thấy hợp đồng");
                }

                foreach (var request in requests)
                {
                    var payment = new PaymentPeriod()
                    {
                        FeePaymentDate = request.FeePaymentDate,
                        Money = request.Money,
                        HDBH = request.HDBH,
                        InsuranceContract = insurance
                    };

                    _context.PaymentPeriods.Add(payment);
                    sum += request.Money;
                }
                if (sum == insurance.InsuranceFee)
                {
                    int result = await _context.SaveChangesAsync();
                    if (result <= 0)
                    {
                        return BadRequest("Something went wrong, can't add them");
                    }
                    return Ok(requests);
                }
                return BadRequest("Tổng số tiền các kỳ đóng bé hơn phí bảo hiểm");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/PaymentPeriod")]
        public async Task<IActionResult> GetPaymentPeriods(string HDBH)
        {
            try
            {
                var paymentPeriods = await _context.PaymentPeriods
                    .Include(c => c.InsuranceContract)
                    .Where(c => c.HDBH == HDBH)
                    .ToListAsync();
                if (paymentPeriods != null)
                {
                    var result = paymentPeriods.Select(s => new PaymentPeriodRequest
                    {
                        HDBH = s.HDBH,
                        FeePaymentDate = s.FeePaymentDate,
                        Money = s.Money
                    });
                    return Ok(result);
                }
                return BadRequest("infoCBNV not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}