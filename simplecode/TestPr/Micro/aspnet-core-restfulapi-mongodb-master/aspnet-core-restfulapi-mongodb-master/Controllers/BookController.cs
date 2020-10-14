using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AwesomeAPI.Models;
using AwesomeAPI.Services;

namespace AwesomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            return _bookService.Get();
        }
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(string id)
        {
            var todoItem = _bookService.Get(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
        [HttpPost]
        public ActionResult<Book> Post(Book item)
        {
            _bookService.Create(item);
            return CreatedAtAction(nameof(GetBook), new { id = item.Id }, item);
        }
    }
}