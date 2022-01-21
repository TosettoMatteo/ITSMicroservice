using BorrowingService.Entities;
using BorrowingService.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BorrowingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IBorrowingRepository _repository;
        private readonly ILogger<BorrowingController> _logger;

        public BorrowingController(IBorrowingRepository repository, ILogger<BorrowingController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }



        // GET: api/<Borrowing>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Borrowing>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Borrowing>>> GetBorrowing()
        {
            var borrowing = await _repository.GetBorrowing();
            return Ok(borrowing);
        }

        // GET api/<Borrowings>/5
        [HttpGet("{id:length(32)}", Name = "GetBorrowing")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Borrowing), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Borrowing>> GetBorrowingById(string id)
        {
            var borrowing = await _repository.GetBorrowing(id);

            if (borrowing == null)
            {
                _logger.LogError($"Il prestito con ID: {id}, non è stato trovato");
                return NotFound();
            }

            return Ok(borrowing);
        }

        // POST api/<Borrowing>
        [HttpPost]
        [ProducesResponseType(typeof(Borrowing), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Borrowing>> CreateBorrowing([FromBody] Borrowing borrowing)
        {
            await _repository.CreateBorrowing(borrowing);

            return CreatedAtRoute("GetBorrowing", new { id = borrowing.Id }, borrowing);
        }

        // PUT api/<Borrowing>/5
        [HttpPut]
        [ProducesResponseType(typeof(Borrowing), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBorrowing([FromBody] Borrowing borrowing)
        {
            return Ok(await _repository.UpdateBorrowing(borrowing));
        }

        // DELETE api/<Borrowing>/5
        [HttpDelete("{id:length(32)}", Name = "DeleteBorrowing")]
        [ProducesResponseType(typeof(Borrowing), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBorrowingById(string id)
        {
            return Ok(await _repository.DeleteBorrowing(id));
        }
    }
}

