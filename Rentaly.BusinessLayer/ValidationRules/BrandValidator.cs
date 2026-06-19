using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

using FluentValidation;
using Rentaly.EntityLayer.Entities;
// Not: Brand sınıfınız hangi katmandaysa onun da using'ini eklemelisiniz. 
// Örneğin: using Rentaly.EntityLayer.Concrete; gibi.

namespace Rentaly.BusinessLayer.ValidationRules
{
    // 1. internal yerine public yaptık
    // 2. AbstractValidator<Brand> diyerek FluentValidation özelliklerini kazandırdık
    public class BrandValidator : AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            // Doğrulama kurallarınızı bu constructor (yapıcı metot) içine yazmalısınız.
            // Örnek kurallar:
            RuleFor(x => x.BrandName)
                .NotEmpty().WithMessage("Marka adı boş geçilemez.")
                .MinimumLength(2).WithMessage("Marka adı en az 2 karakter olmalıdır.");
        }
    }
}