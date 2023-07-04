using BackendServer.Data.EF;
using BackendServer.Data.Enums;
using BackendServer.DTO;
using BackendServer.Models.AnnexContractViewModel;
using BackendServer.validator.AnnexContract;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Exception = System.Exception;

namespace BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnexContractController : ControllerBase
    {
        private readonly ILogger<AnnexContractController> _logger;
        private readonly BHPNTDbContext _context;

        public AnnexContractController(ILogger<AnnexContractController> logger, BHPNTDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("GetList")]
        public async Task<IActionResult> Index(int skipCount = 0, int maxResultCount = 10)
        {
            try
            {
                var totalCount = await _context.AnnexContracts.CountAsync();
                var pagedData = await _context.AnnexContracts
                        .Include(c => c.InfoCBNV)
                        .ThenInclude(c => c.Branch)
                        .Include(c => c.InsuranceContract)
                        .OrderBy(ic => ic.HDPL)
                        .Skip(skipCount)
                        .Take(maxResultCount)
                        .ToListAsync();

                if (pagedData == null)
                {
                    return BadRequest(new ApiErrorResult<AnnexContractRequest>("Không tồn tại hợp đồng phụ lục nào cả"));
                }

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
                    AnnexBeneficiary = ic.InsuranceContract.InsuranceBeneficiary,
                    TVTTCode = ic.TVTTCode,
                    NameTVTT = ic.InfoCBNV.NameTVTT,
                    BranchName = ic.InfoCBNV.Branch.BranchName,
                    HDBH = ic.HDBH,
                    InsuranceType = ic.InsuranceContract.InsuranceType
                });

                var pagedList = new PagedList<AnnexContractRequest>(true, "Success", pagedDataRequest.ToList(), totalCount, skipCount, maxResultCount);
                return Ok(pagedList);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<AnnexContractRequest>(ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpGet("GetSingleAnnex")]
        public async Task<IActionResult> GetOneInsurance(string HDPL)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiErrorResult<AnnexContractRequest>(ModelState.ToString()));
                }

                var AnnexContract = await _context.AnnexContracts
                    .Include(c => c.InfoCBNV)
                        .ThenInclude(c => c.Branch)
                    .Include(c => c.InsuranceContract)
                    .FirstOrDefaultAsync(x => x.HDPL == HDPL);

                if (AnnexContract != null)
                {
                    var AnnexRequest = new AnnexContractRequest
                    {
                        HDPL = AnnexContract.HDPL,
                        AnnexPerson = AnnexContract.AnnexPerson,
                        AdditionalAnnexFee = AnnexContract.AdditionalAnnexFee,
                        AnnexFeeVAT = AnnexContract.AnnexFeeVAT,
                        FromDate = AnnexContract.FromDate,
                        ToDate = AnnexContract.ToDate,
                        Beneficiaries = AnnexContract.Beneficiaries,
                        Status = AnnexContract.Status,
                        AnnexBeneficiary = AnnexContract.InsuranceContract.InsuranceBeneficiary,
                        TVTTCode = AnnexContract.TVTTCode,
                        NameTVTT = AnnexContract.InfoCBNV.NameTVTT,
                        BranchName = AnnexContract.InfoCBNV.Branch.BranchName,
                        HDBH = AnnexContract.HDBH,
                        InsuranceType = AnnexContract.InsuranceContract.InsuranceType
                    };
                    return Ok(new ApiSuccessResult<AnnexContractRequest> { IsSuccess = true, Message = "Success", ResultObj = AnnexRequest });
                }

                return BadRequest(new ApiErrorResult<AnnexContractRequest>("Không tìm thấy hợp đồng phụ lục"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<AnnexContractRequest>(ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("CreateAnnexContract")]
        public async Task<IActionResult> CreateInsurance([FromBody] AnnexContractNewRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiErrorResult<AnnexContract>(ModelState.ToString()));
                }

                var validator = new AnnexContractValidator();
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new ApiErrorResult<AnnexContract>(validationResult.Errors.ToString()));
                }

                var CBNV = await _context.InfoCBNVs.FirstOrDefaultAsync(x => x.TVTTCode == request.TVTTCode);
                if (CBNV == null)
                {
                    return BadRequest(new ApiErrorResult<AnnexContract>("Không tìm thấy cán bộ nhân viên"));
                }
                var insurance = await _context.InsuranceContracts
                    .Include(c => c.AnnexContract)
                    .FirstOrDefaultAsync(x => x.HDBH == request.HDBH);
                if (insurance == null)
                {
                    return BadRequest(new ApiErrorResult<AnnexContract>("Không tìm thấy hợp đồng bảo hiểm"));
                }
                if (insurance.HDPL != null)
                {
                    return BadRequest(new ApiErrorResult<AnnexContract>("Hợp đồng bảo hiểm đã có hợp đồng phụ lục"));
                }

                var checkAnnex = await _context.AnnexContracts.FirstOrDefaultAsync(x => x.HDPL == request.HDPL);
                if (checkAnnex != null)
                {
                    return BadRequest(new ApiErrorResult<AnnexContract>("AnnexContracts đã tồn tại"));
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
                    return BadRequest(new ApiErrorResult<AnnexContract>("Something went wrong, can't add it"));
                }

                return Ok(new ApiSuccessResult<AnnexContract> { IsSuccess = true, Message = "Success", ResultObj = annexContract });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<AnnexContract>(ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpPut("EditAnnexContract")]
        public async Task<IActionResult> EditInsurance(string HDPL, [FromBody] AnnexContractNewRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiErrorResult<AnnexContract>(ModelState.ToString()));
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
                    return BadRequest(new ApiErrorResult<AnnexContract>("Không tìm thấy cán bộ nhân viên"));
                }
                if (insurance == null)
                {
                    return BadRequest(new ApiErrorResult<AnnexContract>("Không tìm thấy hợp đồng bảo hiểm"));
                }
                if (AnnexContract == null)
                {
                    return BadRequest(new ApiErrorResult<AnnexContract>("Không tìm thấy hợp đồng phụ lục"));
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
                    return BadRequest(new ApiErrorResult<AnnexContract>("Something went wrong, can't add it"));
                }

                return Ok(new ApiSuccessResult<AnnexContract> { IsSuccess = true, Message = "Success", ResultObj = AnnexContract });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<AnnexContract>(ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpPut("EditStatus")]
        public async Task<IActionResult> Approve([FromBody] AnnexContractsIdDto annexContractsDto)
        {
            var innexContractsId = await _context.AnnexContracts.FirstOrDefaultAsync(x => x.HDPL == annexContractsDto.HDPL);
            if (innexContractsId == null)
                return BadRequest();

            if (innexContractsId.Status == Insuranceapprove.Pending.ToString())
            {
                return Ok(new { Message = "Hồ sơ đã gửi phê duyệt, không gửi nhiều lần", MessageStatus = "alreadyApproveProcess", Contract = innexContractsId });
            }

            if (innexContractsId.Status == Insuranceapprove.Rejected.ToString())
            {
                return BadRequest(new { Message = "Hồ sơ có trạng thái là từ chối, không thể duyệt" });
            }

            if (innexContractsId.Status == Insuranceapprove.Approved.ToString())
            {
                return Ok(new { Message = "Hồ sơ đã được duyệt, không thể duyệt lại", MessageStatus = "alreadyApproved", Contract = innexContractsId });
            }

            if (innexContractsId.Status == Insuranceapprove.DontSeedapproval.ToString())
            {
                innexContractsId.Status = Insuranceapprove.Pending.ToString();
                _context.AnnexContracts.Update(innexContractsId);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Chuyển duyệt thành công, đợi cấp quản lý duyệt", MessageStatus = "approveProcess", Contract = innexContractsId });
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPut("ApprovedByManagement")]
        public async Task<IActionResult> ApprovedByManagement([FromBody] AnnexContractsDto annexContractsDto)
        {
            var innexContractsId = await _context.AnnexContracts.FirstOrDefaultAsync(x => x.HDPL == annexContractsDto.HDPL);
            if (innexContractsId == null)
                return BadRequest();

            if (innexContractsId.Status == Insuranceapprove.Rejected.ToString())
            {
                return BadRequest(new { Message = "Hồ sơ đã bị từ chối, không thể phê duyệt lại", MessageStatus = "alreadyRejected", Contract = innexContractsId });
            }

            if (innexContractsId.Status == Insuranceapprove.Approved.ToString())
            {
                return BadRequest(new { Message = "Hợp đồng đã được xử lý, không xử lý lại" });
            }

            if (innexContractsId.Status == Insuranceapprove.DontSeedapproval.ToString())
            {
                return BadRequest(new { Message = "Chưa được duyệt hợp đồng này, vui lòng liên hệ TVTT để biết thêm chi tiết" });
            }

            if (innexContractsId.Status == Insuranceapprove.Pending.ToString())
            {
                innexContractsId.Status = annexContractsDto.status;
                _context.AnnexContracts.Update(innexContractsId);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Đã duyệt hợp đồng thành công", MessageStatus = "approveSuccess", Contract = innexContractsId });
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPut("resetStatus")]
        public IActionResult resetStatus()
        {
            var entities = _context.AnnexContracts.ToList();

            foreach (var entity in entities)
            {
                entity.Status = Insuranceapprove.DontSeedapproval.ToString();
            }

            _context.SaveChanges();

            return Ok("đã reset tất cả các hợp đồng về trạng thái chưa gửi chuyển duyệt");
        }
    }
}