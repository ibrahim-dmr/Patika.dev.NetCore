using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.FirstName).NotEmpty().WithMessage("İsim boş olamaz.");
            RuleFor(command => command.Model.LastName).NotEmpty().WithMessage("Soyisim boş olamaz.");
            RuleFor(command => command.Model.DateOfBirth).LessThan(DateTime.Now).WithMessage("Doğum tarihi geçerli olmalı.");
        }
    }

}
