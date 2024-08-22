using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
         private readonly BookStoreDBContext _context;

        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        
        [Fact]
        public void WhenGivenAuthorIdIsNotinDB_InvalidOperationsException_ShouldBeReturn()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId=0;

            
              FluentActions.Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadı.");
        }

        [Fact] 
         public void WhenGivenAuthorIdIsinDB_InvalidOperationException_ShouldBeReturn()
         { 
             UpdateAuthorCommand command =new UpdateAuthorCommand(_context);

             command.Model= new UpdateAuthorModel() {Name="Namık", Surname= "Kemal",DateOfBirth = "21.12.1840" };
             command.AuthorId = 1;

               FluentActions.Invoking(()=> command.Handle()).Invoke();

               var author = _context.Authors.SingleOrDefault(author=> author.Id == command.AuthorId);
               author.Should().NotBeNull(null);
         }

    }
}