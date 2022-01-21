using BookServiceITS.Repository.Interfaces;
using BookServiceITS.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookRepository repository, ILogger<BookController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }



        // GET: api/<Books>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        {
            var book = await _repository.GetBook();
            return Ok(book);
        }

        // GET api/<Borrowings>/5
        [HttpGet("{id:length(32)}", Name = "GetBook")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Book>> GetBookById(string id)
        {
            var book = await _repository.GetBook(id);

            if (book == null)
            {
                _logger.LogError($"Il libro con ID: {id}, non è stato trovato");
                return NotFound();
            }

            return Ok(book);
        }

        // POST api/<Books>
        [HttpPost]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Book>> CreateBorrowing([FromBody] Book book)
        {
            await _repository.CreateBook(book);

            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }

        // PUT api/<Book>/5
        [HttpPut]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBook([FromBody] Book book)
        {
            return Ok(await _repository.UpdateBook(book));
        }

        // DELETE api/<Book>/5
        [HttpDelete("{id:length(32)}", Name = "DeleteBook")]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBookById(string id)
        {
            return Ok(await _repository.DeleteBook(id));
        }
    }
}