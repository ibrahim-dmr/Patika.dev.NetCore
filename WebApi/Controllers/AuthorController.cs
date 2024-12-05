using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthors.GetAuthorDetail;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DBOperations;
using WebApi.Entities;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthor(int id)
        { 
            //var author = _context.Authors.SingleOrDefault(x => x.Id == id);
            //if (author == null)
            //    return NotFound("Yazar Bulunamadı");
            //return Ok(author);

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;

            GetAuthorDetailValidator validator = new GetAuthorDetailValidator();
            validator.ValidateAndThrow(query);

            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] CreateAuthorModel model)
        {

            //var author = _mapper.Map<Author>(model);
            //_context.Authors.Add(author);
            //_context.SaveChanges();
            //return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = model;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel model)
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == id);
            if (author == null)
                return NotFound("Yazar Bulunamadı");
            _mapper.Map(model, author);
            _context.SaveChanges();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == id);
            if (author is null)
                return NotFound("Yazar Bulunamadı");

            if (_context.Books.Any(x => x.AuthorId == id && x.IsActive))
                return BadRequest("Bu yazara ait yayında olan kitaplar bulunduğundan dolayı yazar silinemez.");

            _context.Authors.Remove(author);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
