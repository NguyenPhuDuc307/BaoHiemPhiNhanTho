using BaoHiemPhiNhanTho.BackendServer.Models;
using FluentValidation;

namespace BackendServer.validator.CanBoNhanVien
{
    public class CBNVValidator : AbstractValidator<InfoCBNV>
    {
        public CBNVValidator()
        {
            RuleFor(x => x.TVTTCode).NotEmpty().WithMessage("Mã tư vấn trực tiếp không được để trống");
            RuleFor(x => x.NameTVTT).NotEmpty().WithMessage("Tên tư vấn trực tiếp không được để trống");
            RuleFor(x => x.InfoCBNVBranchCode).NotEmpty().WithMessage("Thông tin cán bộ nhân viên tại chi nhánh không được để trống");
        }
    }
}
