using FluentValidation;

namespace WebApi.Application.BookOperations.CreateBook
{
    // CreateBookCommandValidator sınıfı CreateBookCommand'ın objelerini Valide eder.
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand> 
    {
        public CreateBookCommandValidator() 
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(comman => comman.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.Publishdate).NotEmpty().LessThan(DateTime.Now.Date); 
            RuleFor(comman => comman.Model.Title).NotEmpty().MinimumLength(2);
        }
    }
}
