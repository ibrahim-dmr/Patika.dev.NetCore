using AutoMapper;
using WebApi.Application.BookOperations.GetBookDetail;
using WebApi.Application.BookOperations.GetBooks;
using WebApi.Application.BookOperations.UpdateBook;
using WebApi.Entities;
using static WebApi.Application.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile    // Artık normal bir sınıf değil AutoMapper tarafından bir config dosyası olarak görülecek
    {

        // Bu constructor'da ne neye dönüşebilir bunun configlerini vericem
        public MappingProfile() 
        {
            CreateMap<CreateBookModel, Book>();  // CreateBookModel objesi Book objesine map'lenebilir olsun
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            // BooksViewModel içerisindeki Genre'yı sana verdiğim config ile maple MapFrom yani neyden mapleyeceğini söylüyorum 
            // source üzerindeki GenreId'yi test ederek GenreEnum'a dönüştür GenreEnum'ın string karşılığını bana getir.
        }

    }
}
