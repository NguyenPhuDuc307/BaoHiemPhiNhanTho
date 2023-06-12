using BackendServer.Data.EF;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly BHPNTDbContext _context;

        public CustomerController(ILogger<CustomerController> logger, BHPNTDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("get/Customer")]
        public async Task<IActionResult> GetOneCustomer(string cif)
        {
            try
            {
                var customer = await _context.Customers
                .Include(c => c.InsuranceContracts)
                .FirstOrDefaultAsync(c => c.Cif == cif);

                if (customer != null)
                {
                    return Ok(customer);
                }

                return BadRequest("Customer not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}