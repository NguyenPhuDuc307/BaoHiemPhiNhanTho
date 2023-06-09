﻿using BackendServer.Models.AnnexContractViewModel;
using FluentValidation;

namespace BackendServer.validator.AnnexContract
{
    public class ChuyenDichVMValidator : AbstractValidator<ChuyenDichVM>
    {
        public ChuyenDichVMValidator()
        {
            RuleFor(x => x.HDPL).NotEmpty().WithMessage("Hợp đồng phụ lực không được để trống");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Trạng thái không được để trống");


        }
    }
}
