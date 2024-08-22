using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperation.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenAuthorIdIsNotinDB_InvalidOperationsException_ShouldBeReturn()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId=0;
   
              FluentActions.Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek Yazar Bulunamadı.");
        }

        [Fact]
         
            public void WhenGivenBookIdIsinDB_InvalidOperationException_ShouldBeReturn()
            { 
                DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
                command.AuthorId=1;

                  FluentActions.Invoking(()=> command.Handle()).Invoke();

                  var author = _context.Authors.SingleOrDefault(author=> author.Id == command.AuthorId);
                  author.Should().Be(null);
            }

    }
}