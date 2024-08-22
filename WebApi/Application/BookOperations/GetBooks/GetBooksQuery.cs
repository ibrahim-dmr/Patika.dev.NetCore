using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        // Sadece constructor içinde set edilmesini istiyorum
        private readonly BookStoreDBContext _dbContext;
        public GetBooksQuery(BookStoreDBContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                vm.Add(new BooksViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublisDate.Date.ToString("dd/mm/yyy"),
                    PageCount = book.PageCount,
                });
            }
            return vm;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate{ get; set; }
        public string Genre { get; set; }
    }
}
