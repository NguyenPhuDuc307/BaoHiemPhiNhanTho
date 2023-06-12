using BackendServer.Data.EF;
using BackendServer.Models.BranchViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                    return Ok(result);
                }
                return BadRequest("Branches not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("get/SingleBranch")]
        public async Task<IActionResult> GetOneCBNV(string BranchCode)
        {
            try
            {
                var branch = await _context.Branches.FindAsync(BranchCode);
                if (branch != null)
                {
                    var result = new BranchRequest
                    {
                        BranchCode = branch.BranchCode,
                        BranchName = branch.BranchName,
                    };
                    return Ok(result);
                }
                return BadRequest("branch not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}