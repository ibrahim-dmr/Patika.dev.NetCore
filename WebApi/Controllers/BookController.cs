using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.Application.BookOperations.DeleteBook;
using WebApi.Application.BookOperations.GetBookDetail;
using WebApi.Application.BookOperations.GetBooks;
using WebApi.Application.BookOperations.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {

        private readonly BookStoreDBContext _context;

        // AutoMapper Dependency injection paketine sahip olduğumuzdan injection yöntemiyle IMapper'ı kullanabileceğiz  
        private readonly IMapper _mapper;

        public BookController(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                query.BookId = id;
                result = query.Handle();

            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);        
            }
            return Ok(result);
        }


        //post
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            try
            {
                command.Model = newBook;

                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

                //ValidationResult result = validator.Validate(command);
                //if(!result.IsValid)
                //    foreach (var item in result.Errors)
                //    {
                //        Console.WriteLine("Özellik: " + item.PropertyName + "- Error Message: " + item.ErrorMessage);
                //    }
                //else
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();



            // if(!result.IsValid)
            //foreach (var item in result.Errors)
            //Console.WriteLine("Özellik:"+ item.PropertyName + "- Error Message:"+item.ErrorMessage);
            //else
            //command.Handle();


        }
        //put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {

            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Model = updatedBook;
                command.BookId = id;
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.BookId = id;
                command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message); 
            }
            
            return Ok();
        }
    }
}