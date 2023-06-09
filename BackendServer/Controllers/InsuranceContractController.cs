using BackendServer.Data.EF;
using BackendServer.Models.HopDongPhuLucVM;
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
            var pagedData = await _context.InsuranceContracts.Include(c => c.Customer)
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
                InsurancePartnerCode = ic.InsurancePartnerCode,
                CollateralRef = ic.CollateralRef,
                CustomerName = ic.Customer.Name,
            });

            var pagedList = new PagedList<InsuranceContractRequest>(pagedDataRequest.ToList(), totalCount, page, pageSize);
            return new ApiSuccessResult<PagedList<InsuranceContractRequest>>(pagedList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInsurance(InsuranceContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Cif == request.Cif);

            var CBNV = await _context.InfoCBNVs.FirstOrDefaultAsync(x => x.TVTTCode == request.TVTTCode);

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
                InsurancePartnerCode = request.InsurancePartnerCode,
                CollateralRef = request.CollateralRef,
                Customer = customer,
                InfoCBNV = CBNV
            };

            _context.InsuranceContracts.Add(insurance);
            await _context.SaveChangesAsync();

            return Ok(insurance);
        }

        [HttpGet("GetListPartner")]
        public async Task<IActionResult> GetListPartner(int productPage = 1, int pageSize = 10)
        {
            var partner = await _context.Partners.Skip((productPage - 1) * pageSize).Take(pageSize).ToListAsync();
            return Ok(partner);
        }
    }
}