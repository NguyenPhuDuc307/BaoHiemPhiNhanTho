using BackendServer.Models.CustomerViewModel;
using FluentValidation;

namespace BackendServer.validator.Customer
{
    public class CustomerValidator : AbstractValidator<CustomerRequest>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Cif).NotEmpty().WithMessage("Mã Khách hàng không được để trống");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên khách hàng không được để trống");
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Giới tính không được để trống");
            RuleFor(x => x.CCCD).NotEmpty().WithMessage("Mã căn cước công dân không được để trống");
        }
    }
}
