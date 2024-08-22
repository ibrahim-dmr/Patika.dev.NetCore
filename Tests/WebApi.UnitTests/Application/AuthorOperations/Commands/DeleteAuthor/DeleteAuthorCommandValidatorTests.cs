using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using WebApi.Application.AuthorOperation.Commands.DeleteAuthor;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        

        public void WhenInvalidAuthorIdisGiven_Validator_ShouldBeReturnErrors(int authorid)
        {
            
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = authorid;
            
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        } 
       
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
    
        public void WhenInvalidAuthorIdisGiven_Validator_ShouldNotBeReturnErrors(int authorid)
        {
            
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = authorid;
            
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        } 
    }
}       