using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using webApi.Application.AuthorOperations.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;


namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;

        private readonly IMapper _mapper;
  
        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExitAuthorNameSurnameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var author = new Author ()
            { 
                Name = "Ceyhun",
                Surname= "Kilic",
                DateOfBirth= new DateTime(2006,05,10)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context ,_mapper);
            command.Model = new CreateAuthorModel()
            { 
                Name = author.Name,
                Surname = author.Surname,
                DateOfBirth= (author.DateOfBirth).ToString()
            };

            FluentActions
               .Invoking(() => command.Handle())
               .Should().Throw<InvalidOperationException>().And.Message.Should().Be(" Yazar Zaten Mevcut.");
        }

        [Fact]
        public void WhenValidInputsAreaGiven_Author_ShouldBeCreated()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context , _mapper);
            command.Model= new CreateAuthorModel() 
            { 
                Name = " Namık",
                Surname= "Kemal",
                DateOfBirth = "1840.12.21"
            };

            FluentActions.Invoking(()=>command.Handle()).Invoke();

            var author = _context.Authors.SingleOrDefault(author => author.Name ==command.Model.Name &&author.Surname==command.Model.Surname);
            author.Should().NotBeNull();
            author.DateOfBirth.Should().Be(Convert.ToDateTime(command.Model.DateOfBirth));
        }
    }
}