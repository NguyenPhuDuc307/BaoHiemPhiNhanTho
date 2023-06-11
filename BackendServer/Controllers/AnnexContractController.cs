using BackendServer.Data.EF;
using BackendServer.Models.AnnexContractViewModel;
using BackendServer.validator.AnnexContract;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnnexContractController : ControllerBase
    {
        private readonly ILogger<AnnexContractController> _logger;
        private readonly BHPNTDbContext _context;

        public AnnexContractController(ILogger<AnnexContractController> logger, BHPNTDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("GetList")]
        public async Task<ApiResult<PagedList<AnnexContractRequest>>> Index(int page = 1, int pageSize = 10)
        {
            try
            {
                var totalCount = await _context.AnnexContracts.CountAsync();
                var pagedData = await _context.AnnexContracts
                    .Include(c => c.InfoCBNV)
                        .ThenInclude(c => c.Branch)
                    .Include(c => c.InsuranceContract)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var pagedDataRequest = pagedData.Select(ic => new AnnexContractRequest
                {
                    HDPL = ic.HDPL,
                    AnnexPerson = ic.AnnexPerson,
                    AdditionalAnnexFee = ic.AdditionalAnnexFee,
                    AnnexFeeVAT = ic.AnnexFeeVAT,
                    FromDate = ic.FromDate,
                    ToDate = ic.ToDate,
                    Beneficiaries = ic.Beneficiaries,
                    Status = ic.Status,
                    TVTTCode = ic.TVTTCode,
                    NameTVTT = ic.InfoCBNV.NameTVTT,
                    BranchName = ic.InfoCBNV.Branch.BranchName,
                    HDBH = ic.HDBH,
                });

                var pagedList = new PagedList<AnnexContractRequest>(pagedDataRequest.ToList(), totalCount, page, pageSize);
                return new ApiSuccessResult<PagedList<AnnexContractRequest>>(pagedList);

            }
            catch (Exception ex)
            {
                return new ApiErrorResult<PagedList<AnnexContractRequest>>();
            }

        }



        [HttpGet("GetSingleAnnex")]
        public async Task<IActionResult> GetOneInsurance(string HDPL)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var AnnexContract = await _context.AnnexContracts
                    .Include(c => c.InfoCBNV)
                        .ThenInclude(c => c.Branch)
                    .Include(c => c.InsuranceContract)
                    .FirstOrDefaultAsync(x => x.HDPL == HDPL);

                if (AnnexContract != null)
                {
                    var AnnexRequset = new AnnexContractRequest
                    {
                        HDPL = AnnexContract.HDPL,
                        AnnexPerson = AnnexContract.AnnexPerson,
                        AdditionalAnnexFee = AnnexContract.AdditionalAnnexFee,
                        AnnexFeeVAT = AnnexContract.AnnexFeeVAT,
                        FromDate = AnnexContract.FromDate,
                        ToDate = AnnexContract.ToDate,
                        Beneficiaries = AnnexContract.Beneficiaries,
                        Status = AnnexContract.Status,
                        TVTTCode = AnnexContract.TVTTCode,
                        NameTVTT = AnnexContract.InfoCBNV.NameTVTT,
                        BranchName = AnnexContract.InfoCBNV.Branch.BranchName,
                        HDBH = AnnexContract.HDBH,
                    };
                    return Ok(AnnexRequset);
                }

                return BadRequest("AnnexContract Not Found");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateInsurance([FromBody] AnnexContractNewRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var validator = new AnnexContractValidator();
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var CBNV = await _context.InfoCBNVs.FirstOrDefaultAsync(x => x.TVTTCode == request.TVTTCode);
                if (CBNV == null)
                {
                    return BadRequest("CBNV not found");
                }
                var insurance = await _context.InsuranceContracts.FirstOrDefaultAsync(x => x.HDPL == request.HDPL);
                if (insurance == null)
                {
                    return BadRequest("InsuranceContracts not found");
                }

                var annexContract = new AnnexContract()
                {
                    HDPL = request.HDPL,
                    AnnexPerson = request.AnnexPerson,
                    AdditionalAnnexFee = request.AdditionalAnnexFee,
                    AnnexFeeVAT = request.AnnexFeeVAT,
                    FromDate = request.FromDate,
                    ToDate = request.ToDate,
                    Beneficiaries = request.Beneficiaries,
                    Status = request.Status,
                    TVTTCode = request.TVTTCode,
                    HDBH = request.HDBH,
                    InsuranceContract = insurance,
                    InfoCBNV = CBNV
                };

                _context.AnnexContracts.Add(annexContract);
                int result = await _context.SaveChangesAsync();
                if (result <= 0)
                {
                    return BadRequest("Something went wrong, can't add it");
                }

                return Ok(annexContract);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditInsurance(string HDPL, [FromBody] AnnexContractNewRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var AnnexContract = await _context.AnnexContracts
                    .Include(c => c.InfoCBNV)
                        .ThenInclude(c => c.Branch)
                    .Include(c => c.InsuranceContract)
                    .FirstOrDefaultAsync(x => x.HDPL == HDPL);
                var CBNV = await _context.InfoCBNVs.FirstOrDefaultAsync(x => x.TVTTCode == request.TVTTCode);
                var insurance = await _context.InsuranceContracts.FirstOrDefaultAsync(x => x.HDPL == HDPL);
                if (CBNV == null)
                {
                    return BadRequest("CBNV not found");
                }
                if (insurance == null)
                {
                    return BadRequest("InsuranceContracts not found");
                }

                AnnexContract.HDPL = request.HDPL;
                AnnexContract.AnnexPerson = request.AnnexPerson;
                AnnexContract.AdditionalAnnexFee = request.AdditionalAnnexFee;
                AnnexContract.AnnexFeeVAT = request.AnnexFeeVAT;
                AnnexContract.FromDate = request.FromDate;
                AnnexContract.ToDate = request.ToDate;
                AnnexContract.Beneficiaries = request.Beneficiaries;
                AnnexContract.Status = request.Status;
                AnnexContract.TVTTCode = request.TVTTCode;
                AnnexContract.HDBH = request.HDBH;
                AnnexContract.InsuranceContract = insurance;
                AnnexContract.InfoCBNV = CBNV;

                _context.AnnexContracts.Update(AnnexContract);
                int result = await _context.SaveChangesAsync();
                if (result <= 0)
                {
                    return BadRequest("Something went wrong, can't add it");
                }

                return Ok(AnnexContract);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}