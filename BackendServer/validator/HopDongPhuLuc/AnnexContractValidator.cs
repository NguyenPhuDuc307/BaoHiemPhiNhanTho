using BackendServer.Models.HopDongPhuLucVM;
using FluentValidation;

namespace BackendServer.validator.HopDongPhuLuc
{
    public class AnnexContractValidator : AbstractValidator<AnnexContractRequest>
    {
        public AnnexContractValidator() {
            RuleFor(x => x.HDPL).NotEmpty().WithMessage("HDBL is required");
            RuleFor(x => x.NewOrRenewed).NotEmpty().WithMessage("Mới hay cũ ");
            RuleFor(x => x.STBH).NotEmpty().WithMessage("Số tiền bảo hiểm không được để trống ");
            RuleFor(x => x.InsuranceFee).NotEmpty().WithMessage("Phí bảo hiểm không được để trống");
            RuleFor(x => x.NumberOfPayments).NotEmpty().WithMessage(" Số lần trả không được để trống");
            //RuleFor(x => x.FromDate).NotEmpty().WithMessage(" Số lần trả không được để trống");
            //RuleFor(x => x.ToDate).NotEmpty().WithMessage(" Số lần trả không được để trống");
            RuleFor(x => x.Exception).NotEmpty().WithMessage("Ngoại lệ không được để trống");
            RuleFor(x => x.HDBH).NotEmpty().WithMessage("Hợp đồng bảo hiểm không được để trống");
            RuleFor(x => x.TVTTCode).NotEmpty().WithMessage("Mã tư vấn trực tiếp  không được để trống");
            RuleFor(x => x.Cif).NotEmpty().WithMessage("Mã khách hàng không được để trống");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Trạng thái không được để trống");
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Tên khách hàng không được để trống");
        }
       
    }
}
