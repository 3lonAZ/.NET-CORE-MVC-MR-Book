using FluentValidation;
using MR_Book.Areas.Admin.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MR_Book.Models.Validators
{
    public class AdminLoginValidator:AbstractValidator<AdminLog>
    {
        public AdminLoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("İstifadəçi adı boşdur !");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifrə boş ola bilməz !");
        }
    }
}
