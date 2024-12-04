using AutoMapper;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            // Aktif yazarları listele
            var authors = _context.Authors
                .Where(x => x.IsActive)  // Aktif yazarları getiriyoruz
                .OrderBy(x => x.LastName)  // Soyadına göre sıralıyoruz
                .ToList();

            if (authors.Count == 0)
                throw new InvalidOperationException("Aktif yazar bulunamadı!");

            // Yazarları DTO'ya dönüştürme
            return _mapper.Map<List<AuthorsViewModel>>(authors);
        }
    }

    public class AuthorsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }  // Birleştirilmiş FirstName + LastName
        public DateTime DateOfBirth { get; set; }
    }
}
