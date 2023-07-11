using GenericProject.Model;
using GenericProject.Business;
using Microsoft.AspNetCore.Mvc;

namespace GenericProject.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);

            return person == null ? 
                NotFound() : 
                Ok(person);
        }

        // [FromBody] tag assigns the JSON within the request body to the function parameter
        [HttpPost]
        public IActionResult Create([FromBody] Person person)
        {
            return person == null ? 
                BadRequest() : 
                Ok(_personBusiness.Create(person));
        }

        [HttpPut]
        public IActionResult Update([FromBody] Person person)
        {
            return person == null ?
                BadRequest() :
                Ok(_personBusiness.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var person = _personBusiness.FindById(id);

            if(person == null) return BadRequest();

            _personBusiness.Delete(id);

            return NoContent();
        }
    }
}