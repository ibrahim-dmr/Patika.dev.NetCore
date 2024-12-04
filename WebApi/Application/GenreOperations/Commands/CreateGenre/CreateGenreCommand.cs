using System.Data;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model{ get; set; }
        private readonly BookStoreDBContext _context;
    
        public CreateGenreCommand(BookStoreDBContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name || x.Title == Model.Title);
            if (genre is not null)
                throw new InvalidExpressionException("Kitap Türü Zaten Mevcut!");

            genre = new Genre();
            genre.Name = Model.Name;
            genre.Title = Model.Title;
            _context.Genres.Add(genre);
            _context.SaveChanges();

        }

    }

    public class CreateGenreModel
    {

        public string Name { get; set; }
        public string Title { get;  set; }
    }
}
