using BackendServer.Data.EF;
using BackendServer.Models.PartnerViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendServer.Models
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PartnerController : ControllerBase
    {
        private readonly ILogger<PartnerController> _logger;
        private readonly BHPNTDbContext _context;

        public PartnerController(ILogger<PartnerController> logger, BHPNTDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("get/Partners")]
        public async Task<IActionResult> GetCBNV()
        {
            try
            {
                var partner = await _context.Partners.ToListAsync();
                if (partner != null)
                {
                    var result = partner.Select(s => new PartnerRequest
                    {
                        PartnerCode = s.PartnerCode,
                        Name = s.Name,
                    });
                    return Ok(result);
                }
                return BadRequest("infoCBNV not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/SinglePartners")]
        public async Task<IActionResult> GetOneCBNV(string PartnerCode)
        {
            try
            {
                var partner = await _context.Partners.FindAsync(PartnerCode);
                if (partner != null)
                {
                    var result = new PartnerRequest
                    {
                        PartnerCode = partner.PartnerCode,
                        Name = partner.Name,
                    };
                    return Ok(result);
                }
                return BadRequest("infoCBNV not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}