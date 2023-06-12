using BackendServer.Models.AnnexContractViewModel;
using FluentValidation;

namespace BackendServer.validator.HopDongPhuLuc
{
    public class AnnexContractValidator : AbstractValidator<AnnexContractRequest>
    {
        public AnnexContractValidator()
        {
            RuleFor(x => x.HDPL).NotEmpty().WithMessage("HDBL là bắt buộc");
            RuleFor(x => x.AnnexPerson).NotEmpty().WithMessage("Người ký là bắt buộc");
            RuleFor(x => x.AdditionalAnnexFee).NotEmpty().WithMessage("Phí bổ sung là bắt buộc");
            RuleFor(x => x.AnnexFeeVAT).NotEmpty().WithMessage("Phí bổ sung VAT là bắt buộc");
            RuleFor(x => x.FromDate).NotEmpty().WithMessage("Ngày bắt đầu là bắt buộc");
            RuleFor(x => x.ToDate).NotEmpty().WithMessage("Ngày kết thúc là bắt buộc");
            RuleFor(x => x.Beneficiaries).NotEmpty().WithMessage("Người thụ hưởng là bắt buộc");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Trạng thái là bắt buộc");
            RuleFor(x => x.TVTTCode).NotEmpty().WithMessage("Mã TVTT là bắt buộc");
            RuleFor(x => x.NameTVTT).NotEmpty().WithMessage("Tên TVTT là bắt buộc");
            RuleFor(x => x.BranchName).NotEmpty().WithMessage("Tên chi nhánh là bắt buộc");
            RuleFor(x => x.HDBH).NotEmpty().WithMessage("HDBH là bắt buộc");
        }
    }
}