using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDBContext context)
        {
            context.Books.AddRange( 
            new Book{Title = "Learn Startup",GenreId = 1, PageCount = 200, PublisDate = new DateTime(2001, 06, 12),}, 
            new Book{Title = "Herland",      GenreId = 2, PageCount = 250, PublisDate = new DateTime(2010, 05, 23),},
            new Book{Title = "Dune",         GenreId = 1, PageCount = 540, PublisDate = new DateTime(2001, 12, 21),}
            );
        }
    }
}