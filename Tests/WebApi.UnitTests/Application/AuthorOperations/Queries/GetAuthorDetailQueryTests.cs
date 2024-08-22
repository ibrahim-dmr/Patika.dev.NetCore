using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using WebApi.AuthorOperations.GetAuthorDetail;

namespace Application.AuthorOperations.Queries
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mappper;

        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mappper = testFixture.Mapper;
        }
        [Fact]
        public void WhenGivenAuthorIdIsNotinDb_InvalidOperationException_ShouldBeReturn()
        {
            GetAuthorDetailQuery Query = new GetAuthorDetailQuery(_context,_mappper);
            Query.AuthorId=0;

            FluentActions
               .Invoking(() => Query.Handle())
               .Should().Throw<InvalidOperationException>().And.Message.Should().Be(" Yazar Bulunamadı.");
        }

        [Fact]
        public void WhenGivenAuthorIdIsinDB_InvalidOperationException_shouldBeReturn()
        {
           GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mappper);
           query.AuthorId=1;

            var author = _context.Authors.SingleOrDefault(author => author.Id == query.AuthorId);
            author.Should().NotBeNull();  
        }
    } 
}