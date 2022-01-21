using CustumerServiceITS.Entities;
using CustumerServiceITS.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using KafkaProducer;

namespace CustumerServiceITS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustumerController : ControllerBase
    {

        private readonly ICustumerRepository _repository;
        private readonly ILogger<CustumerController> _logger;

        public CustumerController(ICustumerRepository repository, ILogger<CustumerController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }



        // GET: api/<Custumer>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Custumer>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Custumer>>> GetCustumer()
        {
            var custumer = await _repository.GetCustumer();
            return Ok(custumer);
        }

        // GET api/<Custumer>/5
        [HttpGet("{id:length(32)}", Name = "GetCustumer")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Custumer), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Custumer>> GetCustumerById(string id)
        {
            var custumer = await _repository.GetCustumer(id);

            if (custumer == null)
            {
                _logger.LogError($"L'utente con ID: {id}, non è stato trovato");
                return NotFound();
            }

            return Ok(custumer);
        }

        // POST api/<Custumer>
        [HttpPost]
        [ProducesResponseType(typeof(Custumer), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Custumer>> CreateCustumer([FromBody] Custumer custumer)
        {
            await _repository.CreateCustumer(custumer);

            SendToKafka("simpletalk_topic", "messaggiodiprova");

            return CreatedAtRoute("GetCustumer", new { id = custumer.Id }, custumer);

        }

        // PUT api/<Custumer>/5
        [HttpPut]
        [ProducesResponseType(typeof(Custumer), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCustumer([FromBody] Custumer custumer)
        {
            return Ok(await _repository.UpdateCustumer(custumer));
        }

        // DELETE api/<Custumer>/5
        [HttpDelete("{id:length(32)}", Name = "DeleteCustumer")]
        [ProducesResponseType(typeof(Custumer), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCustumerById(string id)
        {
            return Ok(await _repository.DeleteCustumer(id));
        }
    }
}
