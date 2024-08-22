﻿using FluentValidation;

namespace WebApi.Application.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator() 
        {
            RuleFor(command => command.Model.GenreId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(2);
        }

    }
}
