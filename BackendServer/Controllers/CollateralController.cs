using BackendServer.Data.EF;
using BackendServer.Models.CollateralViewModel;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendServer.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollateralController : ControllerBase
    {
        private readonly ILogger<CollateralController> _logger;
        private readonly BHPNTDbContext _context;

        public CollateralController(ILogger<CollateralController> logger, BHPNTDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("get/Collaterals")]
        public async Task<IActionResult> GetCollateral()
        {
            try
            {
                var collaterals = await _context.Collaterals.ToListAsync();
                if (collaterals != null)
                {
                    var result = collaterals.Select(s => new CollateralRequest
                    {
                        Ref = s.Ref,
                        StatusCollateral = s.StatusCollateral,
                        ValueCollateral = s.ValueCollateral,
                        AddressCollateral = s.AddressCollateral,
                        Relationship = s.Relationship,
                        PropertyType = s.PropertyType,
                        HDBH = s.HDBH,
                    });
                    return Ok(result);
                }
                return BadRequest("Collaterals not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("get/SingleCollateral")]
        public async Task<IActionResult> GetOneCollateral(string Ref)
        {
            try
            {
                var collaterals = await _context.Collaterals.FindAsync(Ref);
                if (collaterals != null)
                {
                    var result = new CollateralRequest
                    {
                        Ref = collaterals.Ref,
                        StatusCollateral = collaterals.StatusCollateral,
                        ValueCollateral = collaterals.ValueCollateral,
                        AddressCollateral = collaterals.AddressCollateral,
                        Relationship = collaterals.Relationship,
                        PropertyType = collaterals.PropertyType,
                        HDBH = collaterals.HDBH,
                    };
                    return Ok(result);
                }
                return BadRequest("Collateral not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Collateral")]
        public async Task<IActionResult> CreateNewCollateral([FromBody] CollateralRequest request)
        {
            try
            {
                var checkCollateral = await _context.Collaterals.FirstOrDefaultAsync(x => x.Ref == request.Ref);
                if (checkCollateral != null)
                {
                    return BadRequest("đã tồn tại tài sản đảm bảo này rồi");
                }

                var checkCollateral1 = await _context.Collaterals.FindAsync(request.HDBH);
                if (checkCollateral1 != null)
                {
                    return BadRequest("đã tồn tại tài sản đảm bảo cho hợp đồng này rồi");
                }

                var collaterals = new Collateral()
                {
                    Ref = request.Ref,
                    StatusCollateral = request.StatusCollateral,
                    ValueCollateral = request.ValueCollateral,
                    AddressCollateral = request.AddressCollateral,
                    Relationship = request.Relationship,
                    PropertyType = request.PropertyType,
                    HDBH = request.HDBH
                };

                _context.Collaterals.Add(collaterals);
                await _context.SaveChangesAsync();
                return Ok(collaterals);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}