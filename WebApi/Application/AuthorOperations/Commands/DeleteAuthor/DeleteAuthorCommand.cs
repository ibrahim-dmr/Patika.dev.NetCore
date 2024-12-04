using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;


namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDBContext _context;

        public DeleteAuthorCommand(BookStoreDBContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.Include(x => x.Books).SingleOrDefault(x => x.Id == AuthorId);
            if (author == null)
                throw new InvalidOperationException("Yazar bulunamadı.");

            if (author.Books.Any(x => x.IsActive))  // Yayında kitap var mı diye kontrol et
                throw new InvalidOperationException("Yazar yayında kitapları olduğu için silinemez.");

            author.IsActive = false;  // Yazar aktifliğini pasif yap
            _context.SaveChanges();
        }
    }

}
