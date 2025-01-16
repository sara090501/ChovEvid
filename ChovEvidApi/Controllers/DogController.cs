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
                return StatusCode(500, $"Chyba pri naèítavaní údajov o psoch: {ex.Message}");
            }
        }

        [HttpDelete("remove/{id}")]
        public IActionResult RemoveDogById(int id)
        {
            try
            {
                _dogRepository.RemoveDogById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Chyba pri odstraòovaní záznamu o psovi: {ex.Message}");
            }
        }
    }
}
