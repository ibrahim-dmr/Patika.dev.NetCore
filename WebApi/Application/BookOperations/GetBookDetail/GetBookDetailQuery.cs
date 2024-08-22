using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDBContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(x => x.Id == BookId).SingleOrDefault();

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");


            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book); // book datasını BookDetailsViewModel'e map'le          

            
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
