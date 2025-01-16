using ChovEvid.Entities;
using ChovEvidApi.Dto;

namespace ChovEvid.Repositories
{
    public interface IBreedingStationRepository
    {
        IEnumerable<BreedingStationDto> GetAll();
        void GenerateBreedingStationDoc(IEnumerable<BreedingStationDto> breedingStations, string filePath);
    }
}
