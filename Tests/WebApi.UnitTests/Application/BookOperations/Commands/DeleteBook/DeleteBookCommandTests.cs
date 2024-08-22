using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mappper;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mappper = testFixture.Mapper;
        }

        public object BookId { get; private set; }

        [Fact]
        public void WhenGivenBookIdIsNotinDB_InvalidOperationsException_ShouldBeReturn()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId=1;
   
            
              FluentActions.Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek Kitap Bulunamadı.");
               
        }
        [Fact]
         
            public void WhenGivenBookIdIsNotinDB_ShouldBeRemove()
            { 
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId=1;

                  FluentActions.Invoking(()=> command.Handle()).Invoke();

                  var book = _context.Books.SingleOrDefault(book=> book.Id == command.BookId);
                  book.Should().Be(null);
            }

    }
}