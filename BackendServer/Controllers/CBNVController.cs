using BackendServer.Data.EF;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Mvc;
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
    }
}