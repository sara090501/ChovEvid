using Microsoft.AspNetCore.Mvc;
using ChovEvid.Repositories;
using ChovEvid.Entities;

namespace ChovEvidApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DogController : ControllerBase
    {
        private readonly IDogRepository _dogRepository;

        public DogController(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        [HttpGet("getAll")]
        public ActionResult<IEnumerable<Dog>> GetAll()
        {
            try
            {
                var dogs = _dogRepository.GetAll();
                return Ok(dogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Chyba pri na��tavan� �dajov o psoch: {ex.Message}");
            }
        }
    }
}
