using Microsoft.AspNetCore.Mvc;
using ChovEvid.Repositories;
using ChovEvid.Entities;

namespace ChovEvidApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BreedingStationController : ControllerBase
    {
        private readonly IBreedingStationRepository _breedingStationRepository;

        public BreedingStationController(IBreedingStationRepository breedingStationRepository)
        {
            _breedingStationRepository = breedingStationRepository;
        }

        [HttpGet("getAll")]
        public ActionResult<IEnumerable<BreedingStation>> GetAll()
        {
            try
            {
                var breedingStations = _breedingStationRepository.GetAll();
                return Ok(breedingStations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Chyba pri načítavaní údajov o chovatežských staniciach: {ex.Message}");
            }
        }
    }
}
