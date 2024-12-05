using AutoMapper;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

namespace WebApi.Common
{
    public class MappingProfile : Profile    // Artık normal bir sınıf değil AutoMapper tarafından bir config dosyası olarak görülecek
    {

        // Bu constructor'da ne neye dönüşebilir bunun configlerini vericem
        public MappingProfile() 
        {
            CreateMap<CreateBookModel, Book>();  // CreateBookModel objesi Book objesine map'lenebilir olsun
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"));

            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            // BooksViewModel içerisindeki Genre'yı sana verdiğim config ile maple MapFrom yani neyden mapleyeceğini söylüyorum 
            // source üzerindeki GenreId'yi test ederek GenreEnum'a dönüştür GenreEnum'ın string karşılığını bana getir.
            
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            CreateMap<CreateAuthorModel, Author>();

            CreateMap<UpdateAuthorModel, Author>();

            CreateMap<Author, AuthorsViewModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth));


            CreateMap<Author, AuthorDetailViewModel>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }

    }
}
