using FluentValidation;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenres;

namespace WebApi.Application.GenreOperations.Queries.GetGenresDetail
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {

        public CreateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(3);
            RuleFor(command => command.Model.Title).NotEmpty().WithMessage("Title is required.");

        }

    }
}
