using BackendServer.Models.AnnexContractViewModel;
using FluentValidation;

namespace BackendServer.validator.AnnexContract
{
    public class AnnexContractValidator : AbstractValidator<AnnexContractNewRequest>
    {
        public AnnexContractValidator()
        {
            RuleFor(x => x.HDPL).NotEmpty().WithMessage("HDBL is required");
            //RuleFor(x => x.FromDate).NotEmpty().WithMessage(" Số lần trả không được để trống");
            //RuleFor(x => x.ToDate).NotEmpty().WithMessage(" Số lần trả không được để trống");
            RuleFor(x => x.HDBH).NotEmpty().WithMessage("Hợp đồng bảo hiểm không được để trống");
            RuleFor(x => x.TVTTCode).NotEmpty().WithMessage("Mã tư vấn trực tiếp  không được để trống");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Trạng thái không được để trống");
        }

    }
}
