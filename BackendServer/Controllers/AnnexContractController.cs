using BackendServer.Data.EF;
using BackendServer.Models.HopDongPhuLucVM;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace BackendServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnexContractController : ControllerBase
    {
        private readonly BHPNTDbContext _context;

        public AnnexContractController(BHPNTDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SaveContract([FromBody] AnnexContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contract = new AnnexContract()
            {
                HDPL = request.HDPL,
                NewOrRenewed = request.NewOrRenewed,
                STBH = request.STBH,
                InsuranceFee = request.InsuranceFee,
                NumberOfPayments = request.NumberOfPayments,
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                Exception = request.Exception,
                HDBH = request.HDBH,
                TVTTCode = request.TVTTCode
            };

            _context.AnnexContracts.Add(contract);
            await _context.SaveChangesAsync();

            return Ok(contract);
        }

        [HttpPut]
        public async Task<IActionResult> ChuyenDich(ChuyenDichVM request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contract = await _context.AnnexContracts.FindAsync(request.HDPL);
            if (contract == null)
            {
                return NotFound();
            }

            contract.Status = request.Status;

            _context.AnnexContracts.Update(contract);
            await _context.SaveChangesAsync();

            return Ok(contract);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int productPage = 1, int pageSize = 10)
        {
            var contracts = await _context.AnnexContracts.Skip((productPage - 1) * pageSize).Take(pageSize).ToListAsync();
            return Ok(contracts);
        }
    }
}