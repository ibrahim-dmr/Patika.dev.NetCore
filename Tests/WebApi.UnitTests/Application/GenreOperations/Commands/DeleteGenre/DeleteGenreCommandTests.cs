using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using webApi.Application.GenreOperations.DeleteGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

     

        [Fact]
        public void WhenGivenGenreIdIsNotinDB_InvalidOperationsException_ShouldBeReturn()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId=0;
   
            
              FluentActions.Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek Kitap Türü Bulunamadı.");
        }

        [Fact]
         
            public void WhenGivenGenreIdIsNotinDB_ShouldBeRemove()
            { 
                DeleteGenreCommand command = new DeleteGenreCommand(_context);
                command.GenreId=1;

                  FluentActions.Invoking(()=> command.Handle()).Invoke();

                  var genre = _context.Genres.SingleOrDefault(genre=> genre.Id == command.GenreId);
                  genre.Should().Be(null);
            }

    }
}