using ChovEvid.Entities;

namespace ChovEvid.Repositories
{
    public interface IDogRepository
    {
        IEnumerable<Dog> GetAll();
    }
}
