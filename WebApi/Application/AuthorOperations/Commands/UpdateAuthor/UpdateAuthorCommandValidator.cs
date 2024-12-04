using FluentValidation;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(x => x.Model.FirstName)
                .NotEmpty().WithMessage("Ad alanı zorunludur.")
                .Length(2, 50).WithMessage("Ad alanı 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.Model.LastName)
                .NotEmpty().WithMessage("Soyad alanı zorunludur.")
                .Length(2, 50).WithMessage("Soyad alanı 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.Model.DateOfBirth)
                .NotEmpty().WithMessage("Doğum tarihi alanı zorunludur.")
                .LessThan(DateTime.Now).WithMessage("Doğum tarihi gelecekte olamaz.");
        }
    }
}
