using FluentAssertions;
using TestSetup;
using webApi.Application.GenreOperations.UpdateGenre;


namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
           
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("abc")]
        [InlineData("ab ")]
        [Theory]

        public void WhenInvalidInputsAreaGiven_Validator_ShouldBeReturnErrors( string genreName)
        { 
             UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model = new UpdateGenreModel(){Name=genreName};

            UpdateGenreCommandValidator validations = new UpdateGenreCommandValidator();
            var result = validations.Validate(command);
            
            result.Errors.Count.Should().BeGreaterThan(0);
        }    

         [InlineData("abcd")]
         [InlineData("abc de")]
         [Theory]
        
        public void WhenInvalidInputsAreaGiven_Validator_ShouldNotBeReturnErrors( string genreName)
        { 
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model = new UpdateGenreModel(){Name=genreName};
        

            UpdateGenreCommandValidator validations = new UpdateGenreCommandValidator();
            var result = validations.Validate(command);
            
            result.Errors.Count.Should().BeGreaterThan(0);
        }    

    }
}        