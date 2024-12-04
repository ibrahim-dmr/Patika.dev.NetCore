using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using System.Linq;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        public int AuthorId { get; set; }

        public GetAuthorDetailQuery(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            // Yazar ve kitap bilgilerini alıyoruz
            var author = _context.Authors
                .Include(x => x.Books)  // Yazarın kitaplarını dahil ediyoruz
                .SingleOrDefault(x => x.Id == AuthorId);

            if (author == null)
                throw new InvalidOperationException("Yazar bulunamadı.");

            // DTO'ya map ediyoruz
            var authorDetail = _mapper.Map<AuthorDetailViewModel>(author);
            // Yazarın kitaplarını ViewModel'e ekliyoruz
            authorDetail.Books = author.Books.Select(b => b.Title).ToList();

            return authorDetail;
        }
    }

    public class AuthorDetailViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }  // Ad ve Soyad birleşimi
        public DateTime DateOfBirth { get; set; }
        public List<string> Books { get; set; }  // Yazarın kitaplarının listesi
    }

}
