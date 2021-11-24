using FluentValidation;
using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Ad alanını boş geçemezsiniz");
            RuleFor(u => u.Surname).NotEmpty().WithMessage("Soyad alanını boş geçemezsiniz");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Mail alanını boş geçemezsiniz");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Geçerli bir e-posta giriniz");
        }
    }
}
