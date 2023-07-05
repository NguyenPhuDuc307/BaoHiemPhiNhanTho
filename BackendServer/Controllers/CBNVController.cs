using BackendServer.Data.EF;
using BackendServer.Models.CBNVViewModel;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BackendServer.Models
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]

    public class CBNVController : ControllerBase
    {
        private readonly ILogger<CBNVController> _logger;
        private readonly BHPNTDbContext _context;

        public CBNVController(ILogger<CBNVController> logger, BHPNTDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("get/CBNV")]
        public async Task<IActionResult> GetOneCBNV(string TVTT)
        {
            try
            {
                var infoCBNV = await _context.InfoCBNVs
                    .Include(c => c.Branch)
                    .FirstOrDefaultAsync(c => c.TVTTCode == TVTT);


                if (infoCBNV != null)
                {
                    var result = new CBNVRequest
                    {
                        TVTTCode = infoCBNV.TVTTCode,
                        NameTVTT = infoCBNV.NameTVTT,
                        BranchCode = infoCBNV.InfoCBNVBranchCode,
                        BranchName = infoCBNV.Branch.BranchName,
                    };
                    return Ok(new ApiSuccessResult<CBNVRequest> { IsSuccess = true, Message = "Success", ResultObj = result });
                }

                return BadRequest(new ApiErrorResult<CBNVRequest>("Không tìm thấy CBNV"));

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<CBNVRequest>(ex.Message));
            }
        }
    }
}