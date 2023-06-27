using BackendServer.Data.EF;
using BackendServer.Models.CollateralViewModel;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

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
        public async Task<ApiResult<IEnumerable<CollateralRequest>>> GetCollateral()
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
                    return new ApiSuccessResult<IEnumerable<CollateralRequest>> { IsSuccess = true, Message = "Success", ResultObj = result };
                }
                return new ApiErrorResult<IEnumerable<CollateralRequest>>("Không tìm thấy tài sản đảm bảo");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<IEnumerable<CollateralRequest>>(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("get/SingleCollateral")]
        public async Task<ApiResult<CollateralRequest>> GetOneCollateral(string Ref)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new ApiErrorResult<CollateralRequest>(ModelState.ToString());
                }

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
                    return new ApiSuccessResult<CollateralRequest> { IsSuccess = true, Message = "Success", ResultObj = result };
                }
                return new ApiErrorResult<CollateralRequest>("Không tìm thấy tài sản đảm bảo");
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<CollateralRequest>(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Collateral")]
        public async Task<ApiResult<Collateral>> CreateNewCollateral([FromBody] CollateralRequest request)
        {
            try
            {
                var checkCollateral = await _context.Collaterals.FirstOrDefaultAsync(x => x.Ref == request.Ref);
                if (checkCollateral != null)
                {
                    return new ApiErrorResult<Collateral>("đã tồn tại tài sản đảm bảo này rồi");
                }

                var checkCollateral1 = await _context.Collaterals.FindAsync(request.HDBH);
                if (checkCollateral1 != null)
                {
                    return new ApiErrorResult<Collateral>("đã tồn tại tài sản đảm bảo cho hợp đồng này rồi");
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
                return new ApiSuccessResult<Collateral> { IsSuccess = true, Message = "Success", ResultObj = collaterals };
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<Collateral>(ex.Message);
            }
        }
    }
}