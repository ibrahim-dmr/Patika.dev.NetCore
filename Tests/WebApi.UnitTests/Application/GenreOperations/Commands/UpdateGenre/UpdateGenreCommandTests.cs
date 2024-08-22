using AutoMapper;
using FluentAssertions;
using TestSetup;
using webApi.Application.GenreOperations.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
         private readonly BookStoreDBContext _context;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        
        [Fact]
        public void WhenGivenGenreIdIsNotinDB_InvalidOperationsException_ShouldBeReturn()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId=0;

            
              FluentActions.Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek Kitap Bulunamadı.");
        }

        [Fact]
        public void WhenGivenNameIsSameWithAnotherGenre_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre(){Name= "Science Fiction"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 2;
            command.Model = new UpdateGenreModel(){Name="Science Fiction"};

             FluentActions.Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı İsimli Kitap Türü Zaten Mevcut.");   
        }


        [Fact] 
         public void WhenGivenGenreIdinDB_ShouldBeUpdate()
         { 
             UpdateGenreCommand command =new UpdateGenreCommand(_context);

             command.Model= new UpdateGenreModel() {Name="WhenGivenGenreIdinDB_ShouldBeUpdate"};
             command.GenreId = 1;

               FluentActions.Invoking(()=> command.Handle()).Invoke();

               var genre = _context.Genres.SingleOrDefault(genre=> genre.Id == command.GenreId);
               genre.Should().NotBeNull();
         }

    }
}