using ChovEvid.Entities;
using ChovEvidApi.Dto;

namespace ChovEvid.Repositories
{
    public interface IDogRepository
    {
        IEnumerable<DogDto> GetAll();
        void RemoveDogById(int id);
    }
}
