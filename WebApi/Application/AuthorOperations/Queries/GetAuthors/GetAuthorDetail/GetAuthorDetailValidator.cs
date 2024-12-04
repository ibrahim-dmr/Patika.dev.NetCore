using FluentValidation;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors.GetAuthorDetail
{
    public class GetAuthorDetailValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailValidator()
        {
            // Yazar ID'si pozitif bir tamsayı olmalı ve 0'dan büyük olmalıdır.
            RuleFor(query => query.AuthorId)
                .GreaterThan(0)
                .WithMessage("Geçerli bir yazar ID'si girin.");
        }
    }
}
