using BackendServer.Data.EF;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly BHPNTDbContext _context;

        public CustomerController(ILogger<CustomerController> logger, BHPNTDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Set<Customer>().Add(customer);
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpGet("get/Customer")]
        public async Task<IActionResult> GetOneCustomer(string cif)
        {
            var customerId = await _context.Customers.FirstOrDefaultAsync(x => x.Cif == cif);
            if (customerId == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.InsuranceContracts)
                .FirstOrDefaultAsync(c => c.Cif == cif);

            if (customer != null)
            {
                return Ok(customer);
            }

            return BadRequest("Customer not found");
        }
    }
}