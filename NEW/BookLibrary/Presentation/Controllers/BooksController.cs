using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _service;

        public BooksController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _service.BookService.GetAllBooksAsync(trackChanges: false);

            return Ok(books);
        }

        [HttpGet("{filter}/{searchTerm?}/{maxValue?}", Name = "BookFilter")]
        public async Task<IActionResult> GetBooksByFilterAsync([FromQuery] bool trackChanges, string filter,
            string searchTerm, string maxValue)
        {
            var books = await _service.BookService.GetBooksByFilterAsync(trackChanges, filter, searchTerm, maxValue);

            return Ok(books);
        }

        [HttpGet("id/{bookId}", Name = "BookById")]
        public async Task<IActionResult> GetBookByIdAsync(string bookId)
        {
            var book = await _service.BookService.GetBookByIdAsync(bookId, trackChanges: false);

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookAsync([FromBody] BookForCreationDto book)
        {
            if (book is null)
            {
                return BadRequest("BookForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var createdBook = await _service.BookService.CreateBookAsync(book);

            return CreatedAtRoute("BookById", new { bookId = createdBook.Id }, createdBook);
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBookAsync(string bookId, [FromBody] BookForUpdateDto book)
        {
            if (book is null)
            {
                return BadRequest("BookForUpdateDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            await _service.BookService.UpdateBookAsync(bookId, book, trackChanges: true);

            return NoContent();
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBookAsync(string bookId)
        {
            await _service.BookService.DeleteBookAsync(bookId, trackChanges: false);
            return NoContent();
        }
    }
}
