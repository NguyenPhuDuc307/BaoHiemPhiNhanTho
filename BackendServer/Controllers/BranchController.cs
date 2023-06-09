using BackendServer.Data.EF;
using BackendServer.Models.BranchViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BackendServer.Models
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]

    public class BranchController : ControllerBase
    {
        private readonly ILogger<BranchController> _logger;
        private readonly BHPNTDbContext _context;

        public BranchController(ILogger<BranchController> logger, BHPNTDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("get/Branches")]
        [Authorize]
        [ServiceFilter(typeof(AuthorizeCustomFilter))]
        public async Task<IActionResult> GetCBNV()
        {
            try
            {
                var branches = await _context.Branches.ToListAsync();
                if (branches != null)
                {
                    var result = branches.Select(s => new BranchRequest
                    {
                        BranchCode = s.BranchCode,
                        BranchName = s.BranchName,
                    });
                    return Ok(new ApiSuccessResult<IEnumerable<BranchRequest>> { IsSuccess = true, Message = "Success", ResultObj = result });
                }
                return BadRequest(new ApiErrorResult<IEnumerable<BranchRequest>>("Branches not found"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<IEnumerable<BranchRequest>>(ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpGet("get/SingleBranch")]
        public async Task<IActionResult> GetOneCBNV(string BranchCode)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiErrorResult<BranchRequest>(ModelState.ToString()));
                }
                var branch = await _context.Branches.FindAsync(BranchCode);
                if (branch != null)
                {
                    var result = new BranchRequest
                    {
                        BranchCode = branch.BranchCode,
                        BranchName = branch.BranchName,
                    };
                    return Ok(new ApiSuccessResult<BranchRequest> { IsSuccess = true, Message = "Success", ResultObj = result });
                }
                return BadRequest(new ApiErrorResult<BranchRequest>("Không tìm thấy chi nhánh"));

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<BranchRequest>(ex.Message));
            }
        }
    }
}