using ProductTrackingSystem.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kategori adını boş geçemezsiniz");
            RuleFor(x => x.Message).MinimumLength(3).WithMessage("En az 3 karakter giriniz");
        }
    }
}
