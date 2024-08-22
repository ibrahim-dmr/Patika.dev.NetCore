using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;


namespace Application.BookOperations.Queries
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mappper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mappper = testFixture.Mapper;
        }
        [Fact]
        public void WhenGivenBookIdIsNotinDb_InvalidOperationException_ShouldBeReturn()
        {
            GetBookDetailQuery bookDetailQuery = new GetBookDetailQuery(_context,_mappper);
            bookDetailQuery.BookId=0;

            FluentActions
               .Invoking(() => bookDetailQuery.Handle())
               .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı.");
        }

        [Fact]
        public void WhenGivenBookIdIsinDB_InvalidOperationException_shouldBeReturn()
        {
           GetBookDetailQuery bookDetailQuery = new GetBookDetailQuery(_context,_mappper);
           bookDetailQuery.BookId=1;

           
            FluentActions.Invoking(()=>bookDetailQuery.Handle()).Invoke();

            //Assert
            var book = _context.Books.SingleOrDefault(book => book.Id == bookDetailQuery.BookId );
            book.Should().NotBeNull();
            
        }
    }
}