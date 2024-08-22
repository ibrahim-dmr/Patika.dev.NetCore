using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
         private readonly BookStoreDBContext _context;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        
        [Fact]
        public void WhenGivenBookIdIsNotinDB_InvalidOperationsException_ShouldBeReturn()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId=0;

            
              FluentActions.Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek Kitap Bulunamadı.");
        }

        [Fact] 
         public void WhenGivenBookIdinDB_ShouldBeUpdate()
         { 
             UpdateBookCommand command =new UpdateBookCommand(_context);

             UpdateBookModel model = new UpdateBookModel() {Title="WhenGivenBookIdinDB_ShouldBeUpdate",GenreId=1 };
             command.Model= model;
             command.BookId = 1;

               FluentActions.Invoking(()=> command.Handle()).Invoke();

               var book = _context.Books.SingleOrDefault(book=> book.Id == command.BookId);
               book.Should().NotBeNull();
         }

    }
}