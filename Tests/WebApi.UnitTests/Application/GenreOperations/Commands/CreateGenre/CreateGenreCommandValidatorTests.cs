
using FluentAssertions;
using TestSetup;
using webApi.Application.GenreOperations.createGenre;
using webApi.Application.GenreOperations.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;


namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("abc")]
        [InlineData("a b")]
        [InlineData("a")]
        [InlineData("ab")]

        public void WhenInvalidInputsGiven_Validator_ShouldBeReturnErrors(String name)
        {
            
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreModel(){Name =name};
            
            CreateGenreCommandValidator validations = new CreateGenreCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        } 
       

        [Theory]
        [InlineData("abcde")]
        [InlineData("abc")]
        [InlineData("ab")]
        [InlineData("querty")]

        
        public void WhenInvalidInputsGiven_Validator_ShouldBeReturn(String name)
        {
            
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreModel(){Name = name};
            
            CreateGenreCommandValidator validations = new CreateGenreCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        } 
    }
}       