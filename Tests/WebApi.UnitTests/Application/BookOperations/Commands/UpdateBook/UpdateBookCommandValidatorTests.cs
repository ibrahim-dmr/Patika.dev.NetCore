using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.UpdateBook;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
           
        [InlineData(1,0,"abcdefg")]
        [InlineData(0,1,"abcdef")]
        [InlineData(1,1,"abc")]
        [InlineData(-1,-1,"abcd")]
        [InlineData(1,1,"")]
        [InlineData(1,1," ")]
        [Theory]

        public void WhenInvalidInputsAreaGiven_Validator_ShouldBeReturnErrors(String title, int bookid , int genreid)
        { 
             UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title =title,
                GenreId = genreid                
            };
            command.BookId = bookid;

            UpdateBookCommandValidator validations = new UpdateBookCommandValidator();
            var result = validations.Validate(command);
            
            result.Errors.Count.Should().BeGreaterThan(0);
        }    

         [InlineData(1,1,"abcd")]
         [Theory]
        
        public void WhenInvalidInputsAreaGiven_Validator_ShouldNotBeReturnErrors(String title, int bookid , int genreid)
        { 
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title =title,
                GenreId = genreid                
            };
            command.BookId = bookid;

            UpdateBookCommandValidator validations = new UpdateBookCommandValidator();
            var result = validations.Validate(command);
            
            result.Errors.Count.Should().BeGreaterThan(0);
        }    

    }
}        