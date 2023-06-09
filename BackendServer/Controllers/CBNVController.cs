using BackendServer.Data.EF;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BackendServer.Models
{
    [Route("api/[controller]")]
    [ApiController]

    public class CBNVController : ControllerBase
    {
        private readonly ILogger<CBNVController> _logger;
        private readonly BHPNTDbContext _context;

        public CBNVController(ILogger<CBNVController> logger, BHPNTDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("get/CBNV")]
        public async Task<IActionResult> GetOneCBNV(string TVTT)
        {
            var infoCBNV = await _context.InfoCBNVs.FindAsync(TVTT);

            if (infoCBNV != null)
            {
                return Ok(infoCBNV);
            }

            return BadRequest("infoCBNV not found");
        }

        [HttpPost("post/CBNV")]
        public async Task<IActionResult> CreateCBNV(InfoCBNV InfoCBNV)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Branch = await _context.Branches.FirstOrDefaultAsync(b => b.BranchCode == InfoCBNV.InfoCBNVBranchCode);
            if (Branch == null)
            {
                return NotFound();
            }
            var CBNV = new InfoCBNV()
            {
                TVTTCode = InfoCBNV.TVTTCode,
                NameTVTT = InfoCBNV.NameTVTT,
                InfoCBNVBranchCode = InfoCBNV.InfoCBNVBranchCode,
                Branch = Branch
            };
            _context.InfoCBNVs.Add(CBNV);
            await _context.SaveChangesAsync();

            return Ok(InfoCBNV);
        }
    }
}