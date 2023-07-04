using BackendServer.Data.EF;
using BackendServer.Data.Entities;
using BackendServer.Models.BrowserInformation;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrowserInformationController : ControllerBase
    {
        private readonly ILogger<BrowserInformationController> _logger;
        private readonly BHPNTDbContext _context;

        public BrowserInformationController(ILogger<BrowserInformationController> logger, BHPNTDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("GetList")]
        public async Task<List<BrowserInformationViewModel>> GetAll()
        {
            var query = from j in _context.browserInformation select new { j };

            return await query
               .Select(p => new BrowserInformationViewModel()
               {
                   browserInformationId = p.j.browserInformationId,
                   name = p.j.name,
                   email = p.j.email,
                   position = p.j.position,
                   area = p.j.area,
                   branch = p.j.branch,
               }).ToListAsync();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> add([FromBody] BrowserInformationViewModel request)
        {
            try
            {
                var checkBI = await _context.browserInformation.FirstOrDefaultAsync(x => x.browserInformationId == request.browserInformationId);
                if (checkBI != null)
                {
                    return BadRequest("BrowserInformation đã tồn tại");
                }

                var newBI = new BrowserInformation()
                {
                    browserInformationId = request.browserInformationId,
                    area = request.area,
                    branch = request.branch,
                    email = request.email,
                    name = request.name,
                    position = request.position,
                };

                _context.browserInformation.AddAsync(newBI);
                int result = await _context.SaveChangesAsync();
                if (result <= 0)
                {
                    return BadRequest("Thêm mới BrowserInformation không thành công");
                }
                return Ok(new ApiSuccessResult<BrowserInformation> { IsSuccess = true, Message = "Success", ResultObj = newBI });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}