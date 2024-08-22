using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using webApi.Application.GenreOperations.DeleteGenre;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        

        public void WhenInvalidGenreIdisGiven_Validator_ShouldBeReturnErrors(int genreid)
        {
            
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = genreid;
            
            DeleteGenreCommandValidator validations = new DeleteGenreCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        } 
       

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
    
        public void WhenInvalidGenreIdisGiven_Validator_ShouldNotBeReturnErrors(int genreid)
        {
            
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = genreid;
            
            DeleteGenreCommandValidator validations = new DeleteGenreCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        } 
    }
}       