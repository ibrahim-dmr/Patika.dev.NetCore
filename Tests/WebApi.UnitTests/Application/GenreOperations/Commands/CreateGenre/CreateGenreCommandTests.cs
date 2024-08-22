using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using webApi.Application.GenreOperations.CreateGenre;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;
  

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenAlreadyExitGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre() { Name = "WhenAlreadyExitGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel() { Name = genre.Name };

            FluentActions
               .Invoking(() => command.Handle())
               .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Zaten Mevcut.");
        }

        [Fact]
        public void WhenValidInputsAreaGiven_Genre_shouldBeCreated()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model= new CreateGenreModel() { Name = "WhenValidInputIsGiven_ShouldBeCreated"};

            FluentActions.Invoking(()=>command.Handle()).Invoke();

        
            var genre = _context.Genres.SingleOrDefault(genre => genre.Title ==command.Model.Name );
            genre.Should().NotBeNull();
           
        }
    }
}