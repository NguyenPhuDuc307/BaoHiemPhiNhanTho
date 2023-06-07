using BackendServer.Data.EF;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Mvc;
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
            var customer = await _context.Customers.FindAsync(cif);

            if (customer != null)
            {
                return Ok(customer);
            }

            return BadRequest("Customer not found");
        }
    }
}