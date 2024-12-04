using FluentAssertions;
using TestSetup;
using webApi.Application.AuthorOperations.CreateAuthor;
using webApi.Application.AuthorOperations.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(",")]
        [InlineData( " " , " " )]
        [InlineData("", "aef" )]
        [InlineData("yy" , "zzz" )]
        [InlineData("aa","a " )]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(String name,string surname)
        {
            
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            command.Model = new CreateAuthorModel()
            { 
                Name = name,
                Surname = surname,
                DateOfBirth= "10.05.2006"
            };
            
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        } 

        [Theory]
        [InlineData("cey","kilic")]
        [InlineData("c","k")]
        [InlineData("cey","kil")]
    
        
        public void WhenInvalidInputsGiven_Validator_ShouldBeReturn(String name , string surname)
        {
            
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            command.Model = new CreateAuthorModel()
            {
                Name = name,
                Surname = surname,
                DateOfBirth= "10.05.2006"
            };

            
            CreateAuthorCommandValidator validations = new CreateAuthorCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        } 
    }
}       