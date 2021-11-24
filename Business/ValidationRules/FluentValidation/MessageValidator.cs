using FluentValidation;
using ProductTrackingSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(m => m.ReceiverMail).NotEmpty().WithMessage("Alıcı adresinizi boş geçemezsiniz");
            RuleFor(m => m.ReceiverMail).EmailAddress().WithMessage("Geçerli bir e-posta giriniz");
            RuleFor(m => m.MessageContent).NotEmpty().WithMessage("Mesajı boş geçemezsiniz");
            RuleFor(m => m.Subject).MinimumLength(2).WithMessage("En az 2 karakter giriniz");
        }
    }
}
