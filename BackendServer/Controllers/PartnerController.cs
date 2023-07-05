using BackendServer.Data.EF;
using BackendServer.Models.PartnerViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BackendServer.Models
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]

    public class PartnerController : ControllerBase
    {
        private readonly ILogger<PartnerController> _logger;
        private readonly BHPNTDbContext _context;

        public PartnerController(ILogger<PartnerController> logger, BHPNTDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
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
                    return Ok(new ApiSuccessResult<IEnumerable<PartnerRequest>> { IsSuccess = true, Message = "Success", ResultObj = result });
                }
                return BadRequest(new ApiErrorResult<IEnumerable<PartnerRequest>>("Branches not found"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<IEnumerable<PartnerRequest>>(ex.Message));
            }
        }

        [AllowAnonymous]
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
                    return Ok(new ApiSuccessResult<PartnerRequest> { IsSuccess = true, Message = "Success", ResultObj = result });
                }
                return BadRequest(new ApiErrorResult<PartnerRequest>("Không tìm thấy chi nhánh"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<PartnerRequest>(ex.Message));
            }
        }
    }
}