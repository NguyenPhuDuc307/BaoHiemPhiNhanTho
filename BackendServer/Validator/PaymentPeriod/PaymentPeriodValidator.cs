using BackendServer.Models.PaymentPeriodViewModel;
using FluentValidation;

namespace BackendServer.validator.HopDongPhuLuc
{
    public class PaymentPeriodValidator : AbstractValidator<PaymentPeriodRequest>
    {
        public PaymentPeriodValidator()
        {
            RuleFor(x => x.FeePaymentDate).NotEmpty().WithMessage("Hợp đồng phụ lực không được để trống");
            RuleFor(x => x.Money).NotEmpty().WithMessage("Trạng thái không được để trống");
            RuleFor(x => x.HDBH).NotEmpty().WithMessage("Trạng thái không được để trống");
        }
    }
}