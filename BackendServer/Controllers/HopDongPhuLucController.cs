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
    public class HopDongPhuLucController : ControllerBase
    {
        private readonly BHPNTDbContext _context;

        public HopDongPhuLucController(BHPNTDbContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return Ok();
        //}

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
    }
}