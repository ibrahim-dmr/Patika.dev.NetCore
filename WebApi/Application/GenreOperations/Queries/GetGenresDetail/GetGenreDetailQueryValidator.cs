﻿using FluentValidation;
using WebApi.Application.GenreOperations.Queries.GetGenres;

namespace WebApi.Application.GenreOperations.Queries.GetGenresDetail
{
    public class GetGenreDetailQueryValidator: AbstractValidator<GetGenreDetailQuery>
    {

        public GetGenreDetailQueryValidator()
        {
            RuleFor(query => query.GenreId).GreaterThan(0);
        }

    }
}
