using GenericProject.Model;
using GenericProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace GenericProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = Ok(_personService.FindById(id));

            return person == null ? 
                NotFound() : 
                Ok(person);
        }

        // [FromBody] tag assigns the JSON within the body of the requisition to the function parameter
        [HttpPost]
        public IActionResult Create([FromBody] Person person)
        {
            return person == null ? 
                BadRequest() : 
                Ok(_personService.Create(person));
        }

        [HttpPut]
        public IActionResult Update([FromBody] Person person)
        {
            return person == null ?
                BadRequest() :
                Ok(_personService.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}