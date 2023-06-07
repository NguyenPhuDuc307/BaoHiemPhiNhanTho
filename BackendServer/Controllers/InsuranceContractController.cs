using BackendServer.Data.EF;
<<<<<<< HEAD
using BackendServer.Models.HopDongPhuLucVM;
=======
>>>>>>> 0d11d8e7551beb8ee9eb734819dd267b731f8272
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceContractController : ControllerBase
    {
        private readonly ILogger<InsuranceContractController> _logger;
        private readonly BHPNTDbContext _context;

        public InsuranceContractController(ILogger<InsuranceContractController> logger, BHPNTDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("GetList")]
        public async Task<ApiResult<PagedList<InsuranceContractRequest>>> Index(int page = 1, int pageSize = 10)
        {
            var totalCount = await _context.InsuranceContracts.CountAsync();
            var pagedData = await _context.InsuranceContracts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var pagedDataRequest = pagedData.Select(ic => new InsuranceContractRequest
            {
                HDBH = ic.HDBH,
                NewOrRenewed = ic.NewOrRenewed,
                STBH = ic.STBH,
                InsuranceFee = ic.InsuranceFee,
                NumberOfPayments = ic.NumberOfPayments,
                FromDate = ic.FromDate,
                ToDate = ic.ToDate,
                Exception = ic.Exception,
                Beneficiaries = ic.Beneficiaries,
                InsuranceType = ic.InsuranceType,
                OtherInsuranceType = ic.OtherInsuranceType,
                InsuranceBeneficiary = ic.InsuranceBeneficiary,
                Cif = ic.Cif,
                TVTTCode = ic.TVTTCode,
                PartnerCode = ic.PartnerCode,
                CollateralRef = ic.CollateralRef,
            }).ToList();

            var pagedList = new PagedList<InsuranceContractRequest>(pagedDataRequest, totalCount, page, pageSize);
            return new ApiSuccessResult<PagedList<InsuranceContractRequest>>(pagedList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInsurance(InsuranceContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var insurance = new InsuranceContract()
            {
                HDBH = request.HDBH,
                NewOrRenewed = request.NewOrRenewed,
                STBH = request.STBH,
                InsuranceFee = request.InsuranceFee,
                NumberOfPayments = request.NumberOfPayments,
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                Exception = request.Exception,
                Beneficiaries = request.Beneficiaries,
                InsuranceType = request.InsuranceType,
                OtherInsuranceType = request.OtherInsuranceType,
                InsuranceBeneficiary = request.InsuranceBeneficiary,
                Cif = request.Cif,
                TVTTCode = request.TVTTCode,
                PartnerCode = request.PartnerCode,
                CollateralRef = request.CollateralRef,
            };

            _context.InsuranceContracts.Add(insurance);
            await _context.SaveChangesAsync();

            return Ok(insurance);
        }
    }

}
