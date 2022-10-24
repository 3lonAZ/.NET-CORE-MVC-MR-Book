using FluentValidation;
using MR_Book.Models.Pages.Order;

namespace MR_Book.Models.Validators
{
    public class OrderDetailValidator : AbstractValidator<OrderDetail>
    {
        public OrderDetailValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Ad və Soyad boş ola bilməz !");
            RuleFor(x => x.ContactNumber).NotEmpty().WithMessage("Əlaqə nömrəsi boş ola bilməz!").Length(12).WithMessage("Əlaqə nömrəsi doğru daxil edilməyib !");
            RuleFor(x => x.Count).NotNull().WithMessage("Kitab sayı boş ola bilməz !").InclusiveBetween(1, 5).WithMessage("1 və 5 arası kitab sayı seçin !");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address boş ola bilməz !").MaximumLength(70).WithMessage("Address 70 hərfdən çox ola bilməz !");
        }
    }
}