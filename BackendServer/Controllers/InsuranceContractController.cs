using BackendServer.Data.EF;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceContractController : ControllerBase
    {
        private readonly ILogger<InsuranceContractController> _logger;
        private readonly BHPNTDbContext _context;

        public InsuranceContractController(ILogger<InsuranceContractController> logger, BHPNTDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("GetList")]
        public async Task<ApiResult<PagedList<InsuranceContract>>> Index(int page = 1, int pageSize = 10)
        {
            var totalCount = await _context.InsuranceContracts.CountAsync();
            var pagedData = await _context.InsuranceContracts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var pagedList = new PagedList<InsuranceContract>(pagedData, totalCount, page, pageSize);
            return new ApiSuccessResult<PagedList<InsuranceContract>>(pagedList);
        }

        [HttpPost("/api/InsuranceContracts/Create")]
        public IActionResult CreateInsurance(InsuranceContract insuranceContract)
        {
            if (ModelState.IsValid)
            {
                _context.Set<InsuranceContract>().Add(insuranceContract);
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest(ModelState);
        }
    }

}
