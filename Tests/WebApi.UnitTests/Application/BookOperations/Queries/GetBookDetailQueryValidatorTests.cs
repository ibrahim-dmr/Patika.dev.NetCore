using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Queries
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(-114)]
        [Theory]
        public void WhenInvalidBookIdisGiven_Validator_ShouldBeReturnErrors(int bookid)
        { 
            GetBookDetailQuery query = new GetBookDetailQuery(null,null);
            query.BookId=bookid;

            GetBookDetailQueryValidator validations = new GetBookDetailQueryValidator();
            var result = validations.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);    
        }

        [InlineData(1)]
        [InlineData(25)]
        [Theory]
        public void WhenInvalidBookIdIsGiven_Validator_ShouldNotBeReturnErrors(int bookid)
        { 
           GetBookDetailQuery query = new GetBookDetailQuery(null,null);
           query.BookId=bookid;  
           
          GetBookDetailQueryValidator validations = new GetBookDetailQueryValidator();
          var result = validations.Validate(query);

            result.Errors.Count.Should().Be(0);
        }      
    }
}