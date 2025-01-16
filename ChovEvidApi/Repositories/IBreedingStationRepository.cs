using ChovEvid.Entities;
using ChovEvidApi.Dto;

namespace ChovEvid.Repositories
{
    public interface IBreedingStationRepository
    {
        IEnumerable<BreedingStationDto> GetAll();
    }
}
