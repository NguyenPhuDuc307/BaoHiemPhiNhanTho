using BackendServer.Data.EF;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BackendServer.Models
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

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
                var infoCBNV = await _context.InfoCBNVs.FindAsync(TVTT);

                if (infoCBNV != null)
                {
                    return Ok(infoCBNV);
                }

                return BadRequest("infoCBNV not found");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // [HttpPost("post/CBNV")]
        // public async Task<IActionResult> CreateCBNV(InfoCBNV InfoCBNV)
        // {

        //     try
        //     {
        //         if (!ModelState.IsValid)
        //         {
        //             return BadRequest(ModelState);
        //         }

        //         var Branch = await _context.Branches.FirstOrDefaultAsync(b => b.BranchCode == InfoCBNV.InfoCBNVBranchCode);
        //         if (Branch == null)
        //         {
        //             return NotFound();
        //         }
        //         var CBNV = new InfoCBNV()
        //         {
        //             TVTTCode = InfoCBNV.TVTTCode,
        //             NameTVTT = InfoCBNV.NameTVTT,
        //             InfoCBNVBranchCode = InfoCBNV.InfoCBNVBranchCode,
        //             Branch = Branch
        //         };
        //         _context.InfoCBNVs.Add(CBNV);
        //         await _context.SaveChangesAsync();

        //         return Ok(InfoCBNV);

        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex);
        //     }
        // }
    }
}