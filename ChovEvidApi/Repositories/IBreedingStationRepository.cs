using ChovEvid.Entities;

namespace ChovEvid.Repositories
{
    public interface IBreedingStationRepository
    {
        IEnumerable<BreedingStation> GetAll();
    }
}
