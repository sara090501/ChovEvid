using Microsoft.AspNetCore.Mvc;
using ChovEvid.Repositories;
using ChovEvid.Entities;
using ChovEvidApi.Dto;

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
        public ActionResult<IEnumerable<BreedingStationDto>> GetAll()
        {
            try
            {
                var breedingStations = _breedingStationRepository.GetAll();
                return Ok(breedingStations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Chyba pri naèítavaní údajov o chovate¾ských staniciach: {ex.Message}");
            }
        }
    }
}
