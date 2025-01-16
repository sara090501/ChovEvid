using Microsoft.AspNetCore.Mvc;
using ChovEvid.Repositories;
using ChovEvid.Entities;

namespace ChovEvidApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet("getAll")]
        public ActionResult<IEnumerable<Person>> GetAll()
        {
            try
            {
                var people = _personRepository.GetAll();
                return Ok(people);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Chyba pri naèítavaní rokov: {ex.Message}");
            }
        }
    }
}
