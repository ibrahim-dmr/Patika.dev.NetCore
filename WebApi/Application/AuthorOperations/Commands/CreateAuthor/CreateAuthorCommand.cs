using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;


        public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public void Handle()
        {
            // Yazarın daha önce eklenip eklenmediğini kontrol et
            var author = _context.Authors.SingleOrDefault(x => x.FirstName == Model.FirstName && x.LastName == Model.LastName);
            if (author is not null)
                throw new InvalidOperationException("Yazar zaten mevcut.");

            // Yazar ekle
            //author = new Author
            //{
            //    FirstName = Model.FirstName,
            //    LastName = Model.LastName,
            //    DateOfBirth = Model.DateOfBirth,
            //};
            //_context.Authors.Add(author);
            //_context.SaveChanges();



            author = _mapper.Map<Author>(Model);     

            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

}



