using FluentValidation;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Queries.GetGenres;

namespace WebApi.Application.GenreOperations.Queries.GetGenresDetail
{
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    {

        public DeleteGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0);
        }

    }
}
