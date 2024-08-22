using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDBContext _dbContext;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(x => x.Id == BookId).SingleOrDefault();

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");


            BookDetailViewModel vm = new BookDetailViewModel();
            vm.Title = book.Title;
            vm.PageCount = book.PageCount;
            vm.PublishDate = book.PublisDate.Date.ToString("dd/mm/yyy");
            vm.Genre = ((GenreEnum)book.GenreId).ToString();
            
            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string  Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
