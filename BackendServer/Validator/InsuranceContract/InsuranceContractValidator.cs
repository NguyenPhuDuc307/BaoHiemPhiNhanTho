using BackendServer.Models.InsuranceContractViewModel;
using FluentValidation;

namespace BackendServer.Validator.InsuranceContract
{
    public class InsuranceContractValidator : AbstractValidator<InsuranceContractNewRequest>
    {
        public InsuranceContractValidator()
        {
            RuleFor(x => x.HDBH).NotEmpty().WithMessage("Hợp đồng bảo hiểm không được để trống");
            RuleFor(x => x.NewOrRenewed).NotEmpty().WithMessage("Hợp đồng mới hay gia hạn không được để trống");
            RuleFor(x => x.STBH).NotEmpty().WithMessage("Số tiền bảo hiểm không được để trống");
            RuleFor(x => x.InsuranceFee).NotEmpty().WithMessage("Phí bảo hiểm không được để trống");
            RuleFor(x => x.NumberOfPayments).NotEmpty().WithMessage("Số lần thanh toán không được để trống");
            RuleFor(x => x.FromDate).NotEmpty().WithMessage("Ngày bắt đầu không được để trống");
            RuleFor(x => x.ToDate).NotEmpty().WithMessage("Ngày kết thúc không được để trống");
            // RuleFor(x => x.Exception).NotEmpty().WithMessage("Trường ngoại lệ không được để trống");
            RuleFor(x => x.Beneficiaries).NotEmpty().WithMessage("Trường người thụ hưởng không được để trống");
            RuleFor(x => x.InsuranceType).NotEmpty().WithMessage("Trường loại bảo hiểm không được để trống");
            // RuleFor(x => x.OtherInsuranceType).NotEmpty().WithMessage("Trường loại bảo hiểm khác không được để trống");
            RuleFor(x => x.InsuranceBeneficiary).NotEmpty().WithMessage("Trường người được bảo hiểm không được để trống");
            RuleFor(x => x.Cif).NotEmpty().WithMessage("Trường CIF không được để trống");
            RuleFor(x => x.TVTTCode).NotEmpty().WithMessage("Trường mã TVTT không được để trống");
            RuleFor(x => x.InsurancePartnerCode).NotEmpty().WithMessage("Trường mã đối tác bảo hiểm không được để trống");
            RuleFor(x => x.CollateralRef).NotEmpty().WithMessage("Trường tham chiếu tài sản không được để trống");
        }
    }
}