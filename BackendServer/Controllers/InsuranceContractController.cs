using BackendServer.Data.EF;
using BackendServer.Data.Enums;
using BackendServer.DTO;
using BackendServer.Models.AnnexContractViewModel;
using BackendServer.Models.InsuranceContractViewModel;
using BackendServer.Models.PaymentPeriodViewModel;
using BaoHiemPhiNhanTho.BackendServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Exception = System.Exception;

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

        [AllowAnonymous]
        [HttpGet("GetList")]
        public async Task<IActionResult> List(int SkipCount = 0, int MaxResultCount = 10)
        {
            try
            {
                // lấy tổng phần tử trong list
                var totalCount = await _context.InsuranceContracts.CountAsync();

                // query
                var query = from j in _context.InsuranceContracts
                            join j1 in _context.Customers on j.Cif equals j1.Cif
                            join j2 in _context.Collaterals on j.Ref equals j2.Ref
                            join j3 in _context.Partners on j.InsurancePartnerCode equals j3.PartnerCode
                            join j4 in _context.InfoCBNVs on j.TVTTCode equals j4.TVTTCode
                            join j5 in _context.Branches on j4.InfoCBNVBranchCode equals j5.BranchCode
                            where j.HDBH == j2.HDBH
                            select new InsuranceContractRequest()
                            {
                                HDBH = j.HDBH ?? null,
                                NewOrRenewed = j.NewOrRenewed ?? null,
                                STBH = j.STBH ?? null,
                                InsuranceFee = j.InsuranceFee ?? null,
                                NumberOfPayments = j.NumberOfPayments ?? null,
                                FromDate = j.FromDate ?? null,
                                ToDate = j.ToDate ?? null,
                                DateFee = j.NumberOfPayments == 1 ? j.ToDate : null,
                                Exception = j.Exception ?? null,
                                Beneficiaries = j.Beneficiaries ?? null,
                                InsuranceType = j.InsuranceType ?? null,
                                OtherInsuranceType = j.OtherInsuranceType ?? null,
                                InsuranceBeneficiary = j.InsuranceBeneficiary ?? null,
                                Status = j.Status ?? null,
                                Cif = j.Cif ?? null,
                                TVTTCode = j.TVTTCode ?? null,
                                InsurancePartnerCode = j.InsurancePartnerCode,
                                CustomerName = j1.Name,
                                CustomerType = j1.CustomerType,
                                CCCD = j1.CCCD,
                                PartnerName = j3.Name,
                                StatusCollateral = j2.StatusCollateral ?? null,
                                CollateralRef = j2.Ref,
                                CollateralType = j2.PropertyType,
                                ValueCollateral = j2.ValueCollateral,
                                AddressCollateral = j2.AddressCollateral,
                                Relationship = j2.Relationship,
                                NameTVTT = j4.NameTVTT,
                                BranchName = j5.BranchName,
                                HDPL = j.HDPL,
                                lstPaymentPeriod = _context.PaymentPeriods
                                    .Where(pp => pp.HDBH == j.HDBH)
                                    .Select(pp => new PaymentPeriodRequest()
                                    {
                                        FeePaymentDate = pp.FeePaymentDate,
                                        Money = pp.Money,
                                        HDBH = pp.HDBH
                                    })
                                    .ToList()
                            };

                var pagedListTotal = new PagedList<InsuranceContractRequest>(true, "Success", query.Skip(SkipCount).Take(MaxResultCount).ToList(), totalCount, SkipCount, query.Skip(SkipCount).Take(MaxResultCount).Count());

                if (pagedListTotal == null)
                {
                    return BadRequest(new ApiErrorResult<PagedList<InsuranceContractRequest>>("Không tìm thấy hợp đồng nào hết"));
                }
                return Ok(new ApiSuccessResult<PagedList<InsuranceContractRequest>>(pagedListTotal));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<PagedList<InsuranceContractRequest>>());
            }
        }

        [AllowAnonymous]
        [HttpGet("GetSingleInsurance")]
        public async Task<IActionResult> GetOneInsurance(string HDBH)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiErrorResult<InsuranceContractRequest>(ModelState.ToString()));
                }

                var InsuranceContract = await _context.InsuranceContracts
                .Include(c => c.Customer)
                .Include(c => c.Collateral)
                .Include(c => c.InfoCBNV)
                    .ThenInclude(c => c.Branch)
                .Include(c => c.AnnexContract)
                .Include(c => c.Partner)
                .Include(c => c.PaymentPeriods)
                .Include(c => c.AnnexContract)
                .FirstOrDefaultAsync(x => x.HDBH == HDBH);

                var paymentperiods = await _context.PaymentPeriods
                    .Include(c => c.InsuranceContract)
                    .Where(c => c.HDBH == HDBH)
                    .ToListAsync();

                var payments = new List<PaymentPeriodRequest>();

                foreach (var item in paymentperiods)
                {
                    var p = new PaymentPeriodRequest()
                    {
                        FeePaymentDate = item.FeePaymentDate,
                        Money = item.Money,
                        HDBH = item.HDBH,
                    };
                    payments.Add(p);
                }

                if (InsuranceContract != null)
                {
                    var InsuranceRequset = new InsuranceContractRequest
                    {
                        HDBH = InsuranceContract.HDBH,
                        NewOrRenewed = InsuranceContract.NewOrRenewed,
                        STBH = InsuranceContract.STBH,
                        InsuranceFee = InsuranceContract.InsuranceFee,
                        NumberOfPayments = InsuranceContract.NumberOfPayments,
                        FromDate = InsuranceContract.FromDate,
                        ToDate = InsuranceContract.ToDate,
                        DateFee = InsuranceContract.NumberOfPayments == 1 ? InsuranceContract.ToDate : null,
                        Exception = InsuranceContract.Exception,
                        Beneficiaries = InsuranceContract.Beneficiaries,
                        InsuranceType = InsuranceContract.InsuranceType,
                        OtherInsuranceType = InsuranceContract.OtherInsuranceType,
                        InsuranceBeneficiary = InsuranceContract.InsuranceBeneficiary,
                        Cif = InsuranceContract.Cif,
                        TVTTCode = InsuranceContract.TVTTCode,
                        InsurancePartnerCode = InsuranceContract.InsurancePartnerCode,
                        CustomerName = InsuranceContract.Customer.Name,
                        CustomerType = InsuranceContract.Customer.CustomerType,
                        CCCD = InsuranceContract.Customer.CCCD,
                        Status = InsuranceContract.Status,
                        PartnerName = InsuranceContract.Partner.Name,
                        StatusCollateral = InsuranceContract.Collateral.StatusCollateral,
                        CollateralRef = InsuranceContract.Collateral.Ref,
                        CollateralType = InsuranceContract.Collateral.PropertyType,
                        ValueCollateral = InsuranceContract.Collateral.ValueCollateral,
                        AddressCollateral = InsuranceContract.Collateral.AddressCollateral,
                        Relationship = InsuranceContract.Collateral.Relationship,
                        NameTVTT = InsuranceContract.InfoCBNV.NameTVTT,
                        BranchName = InsuranceContract.InfoCBNV.Branch.BranchName,
                        HDPL = InsuranceContract.HDPL,
                        lstPaymentPeriod = payments
                    };
                    return Ok(new ApiSuccessResult<InsuranceContractRequest> { IsSuccess = true, Message = "Success", ResultObj = InsuranceRequset });
                }

                return BadRequest(new ApiErrorResult<InsuranceContractRequest>("Không tìm thấy hợp đồng bảo hiểm"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<InsuranceContractRequest>(ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("CreateInsuranceContracWithPeriod")]
        public async Task<IActionResult> CreateInsurancePeriod([FromBody] InsuranceNewWithPeriodsNewRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiErrorResult<InsuranceContract>(ModelState.ToString()));
                }

                var checkInsurance = await _context.InsuranceContracts.FirstOrDefaultAsync(x => x.HDBH == request.HDBH);
                if (checkInsurance != null)
                {
                    return BadRequest(new ApiErrorResult<InsuranceContract>("Hợp đồng bảo hiểm đã tồn tại"));
                }

                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Cif == request.Cif);
                if (customer == null)
                {
                    return BadRequest(new ApiErrorResult<InsuranceContract>("Không tìm thấy khách hàng"));
                }

                var CBNV = await _context.InfoCBNVs.FirstOrDefaultAsync(x => x.TVTTCode == request.TVTTCode);
                if (CBNV == null)
                {
                    return BadRequest(new ApiErrorResult<InsuranceContract>("Không tìm thấy cán bộ nhân viên"));
                }

                var partner = await _context.Partners.FirstOrDefaultAsync(x => x.PartnerCode == request.InsurancePartnerCode);
                if (partner == null)
                {
                    return BadRequest(new ApiErrorResult<InsuranceContract>("Không tìm thấy đối tác"));
                }

                var collateral = await _context.Collaterals.FirstOrDefaultAsync(x => x.Ref == request.CollateralRef);
                if (collateral == null)
                {
                    return BadRequest(new ApiErrorResult<InsuranceContract>("Không tìm thấy tài sản đảm bảo"));
                }

                var checkCollateral = await _context.InsuranceContracts.FirstOrDefaultAsync(c => c.Ref == request.CollateralRef);
                if (checkCollateral != null)
                {
                    return BadRequest(new ApiErrorResult<InsuranceContract>("Tài sản đảm bảo này đã thuộc hợp đồng khác"));
                }

                decimal? sum = 0;
                foreach (var item in request.lstPaymentPeriod)
                {
                    sum += item.Money;
                }

                // Nếu số tiền đóng phí bằng với phí bảo hiểm thì lưu
                if (sum == request.InsuranceFee)
                {
                    var insurance = new InsuranceContract()
                    {
                        HDBH = request.HDBH,
                        NewOrRenewed = request.NewOrRenewed,
                        STBH = request.STBH,
                        InsuranceFee = request.InsuranceFee,
                        NumberOfPayments = request.lstPaymentPeriod.Count,
                        FromDate = request.FromDate,
                        ToDate = request.ToDate,
                        Exception = request.Exception ?? "",
                        Beneficiaries = request.Beneficiaries,
                        InsuranceType = request.InsuranceType,
                        OtherInsuranceType = request.OtherInsuranceType ?? "",
                        InsuranceBeneficiary = request.InsuranceBeneficiary,
                        Status = Insuranceapprove.DontSeedapproval.ToString(),
                        Cif = request.Cif,
                        TVTTCode = request.TVTTCode,
                        InsurancePartnerCode = request.InsurancePartnerCode,
                        Ref = request.CollateralRef,
                        Customer = customer,
                        InfoCBNV = CBNV,
                        Partner = partner,
                        Collateral = collateral
                    };

                    foreach (var item in request.lstPaymentPeriod)
                    {
                        var payment = new PaymentPeriod()
                        {
                            FeePaymentDate = item.FeePaymentDate,
                            Money = item.Money,
                            HDBH = item.HDBH,
                            InsuranceContract = insurance
                        };
                        _context.PaymentPeriods.Add(payment);
                        sum += item.Money;
                    }
                    _context.InsuranceContracts.Add(insurance);
                    int result = await _context.SaveChangesAsync();
                    if (result <= 0)
                    {
                        return BadRequest(new ApiErrorResult<InsuranceContract>("Something went wrong, can't add it"));
                    }
                    return Ok(new ApiSuccessResult<InsuranceContract> { IsSuccess = true, Message = "Success", ResultObj = insurance });
                }
                return BadRequest(new ApiErrorResult<InsuranceContract>("Số tiền các kỳ không bằng số tiền bảo hiểm"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<InsuranceContract>(ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpPut("EditInsuranceContrac")]
        public async Task<IActionResult> EditInsurance(string HDBH, [FromBody] InsuranceNewWithPeriodsNewRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiErrorResult<InsuranceContract>(ModelState.ToString()));
                }
                var insurance = await _context.InsuranceContracts.Include(c => c.Customer)
                    .Include(c => c.Collateral)
                    .Include(c => c.InfoCBNV)
                        .ThenInclude(c => c.Branch)
                    .Include(c => c.AnnexContract)
                    .Include(c => c.Partner)
                    .FirstOrDefaultAsync(x => x.HDBH == HDBH);
                if (insurance == null)
                {
                    return BadRequest(new ApiErrorResult<InsuranceContract>("Không tìm thấy hợp đồng bảo hiểm"));
                }
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Cif == request.Cif);
                if (customer == null)
                {
                    return BadRequest(new ApiErrorResult<InsuranceContract>("Không tìm thấy khách hàng"));
                }

                var CBNV = await _context.InfoCBNVs.FirstOrDefaultAsync(x => x.TVTTCode == request.TVTTCode);
                if (CBNV == null)
                {
                    return BadRequest(new ApiErrorResult<InsuranceContract>("Không tìm thấy cán bộ nhân viên"));
                }

                var partner = await _context.Partners.FirstOrDefaultAsync(x => x.PartnerCode == request.InsurancePartnerCode);
                if (partner == null)
                {
                    return BadRequest(new ApiErrorResult<InsuranceContract>("Không tìm thấy đối tác"));
                }

                var collateral = await _context.Collaterals.FirstOrDefaultAsync(x => x.Ref == request.CollateralRef);
                if (collateral == null)
                {
                    return BadRequest(new ApiErrorResult<InsuranceContract>("Không tìm thấy tài sản đảm bảo"));
                }

                decimal? sum = 0;
                foreach (var item in request.lstPaymentPeriod)
                {
                    sum += item.Money;
                }

                insurance.HDBH = request.HDBH;
                insurance.NewOrRenewed = request.NewOrRenewed;
                insurance.STBH = request.STBH;
                insurance.InsuranceFee = request.InsuranceFee;
                insurance.NumberOfPayments = request.NumberOfPayments;
                insurance.FromDate = request.FromDate;
                insurance.ToDate = request.ToDate;
                insurance.Exception = request.Exception;
                insurance.Beneficiaries = request.Beneficiaries;
                insurance.InsuranceType = request.InsuranceType;
                insurance.OtherInsuranceType = request.OtherInsuranceType;
                insurance.InsuranceBeneficiary = request.InsuranceBeneficiary;
                insurance.Status = Insuranceapprove.DontSeedapproval.ToString();
                insurance.Cif = request.Cif;
                insurance.TVTTCode = request.TVTTCode;
                insurance.InsurancePartnerCode = request.InsurancePartnerCode;
                insurance.Ref = request.CollateralRef;
                insurance.Customer = customer;
                insurance.InfoCBNV = CBNV;
                insurance.Partner = partner;
                insurance.Collateral = collateral;

                _context.InsuranceContracts.Update(insurance);
                int result = await _context.SaveChangesAsync();
                if (result <= 0)
                {
                    return BadRequest(new ApiErrorResult<InsuranceContract>("Something went wrongHDBH, can't add it"));
                }

                return Ok(new ApiSuccessResult<InsuranceContract> { IsSuccess = true, Message = "Success", ResultObj = insurance });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<InsuranceContract>(ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpPut("EditStatus")]
        public async Task<IActionResult> Approve([FromBody] InsuranceIdDto insuranceDto)
        {
            var insuranceId = await _context.InsuranceContracts.FirstOrDefaultAsync(x => x.HDBH == insuranceDto.HDBH);
            if (insuranceId == null)
                return BadRequest();

            if (insuranceId.Status == Insuranceapprove.Pending.ToString())
            {
                return Ok(new { Message = "Hồ sơ đã gửi phê duyệt, không gửi nhiều lần", MessageStatus = "alreadyApproveProcess", Contract = insuranceId });
            }

            if (insuranceId.Status == Insuranceapprove.Rejected.ToString())
            {
                return BadRequest(new { Message = "Hồ sơ có trạng thái là từ chối, không thể duyệt" });
            }

            if (insuranceId.Status == Insuranceapprove.Approved.ToString())
            {
                return Ok(new { Message = "Hồ sơ đã được duyệt, không thể duyệt lại", MessageStatus = "alreadyApproved", Contract = insuranceId });
            }

            if (insuranceId.Status == Insuranceapprove.DontSeedapproval.ToString())
            {
                insuranceId.Status = Insuranceapprove.Pending.ToString();
                _context.InsuranceContracts.Update(insuranceId);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Chuyển duyệt thành công, đợi cấp quản lý duyệt", MessageStatus = "approveProcess", Contract = insuranceId });
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPut("ApprovedByManagement")]
        public async Task<IActionResult> ApprovedByManagement([FromBody] InsuranceDto InsuranceDto)
        {
            var insuranceId = await _context.InsuranceContracts.FirstOrDefaultAsync(x => x.HDBH == InsuranceDto.HDBH);
            if (insuranceId == null)
                return BadRequest();

            if (insuranceId.Status == Insuranceapprove.Rejected.ToString())
            {
                return BadRequest(new { Message = "Hồ sơ đã bị từ chối, không thể phê duyệt lại", MessageStatus = "alreadyRejected", Contract = insuranceId });
            }

            if (insuranceId.Status == Insuranceapprove.Approved.ToString())
            {
                return BadRequest(new { Message = "Hợp đồng đã được xử lý, không xử lý lại" });
            }

            if (insuranceId.Status == Insuranceapprove.DontSeedapproval.ToString())
            {
                return BadRequest(new { Message = "Chưa được duyệt hợp đồng này, vui lòng liên hệ TVTT để biết thêm chi tiết" });
            }

            if (insuranceId.Status == Insuranceapprove.Pending.ToString())
            {
                insuranceId.Status = InsuranceDto.status;
                _context.InsuranceContracts.Update(insuranceId);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Đã duyệt hợp đồng thành công", MessageStatus = "approveSuccess", Contract = insuranceId });
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPut("resetStatus")]
        public IActionResult resetStatus()
        {
            var entities = _context.InsuranceContracts.ToList();

            foreach (var entity in entities)
            {
                entity.Status = Insuranceapprove.DontSeedapproval.ToString();
            }

            _context.SaveChanges();

            return Ok("đã reset tất cả các hợp đồng về trạng thái chưa gửi chuyển duyệt");
        }
    }
}