using ChovEvid.Entities;

namespace ChovEvid.Repositories
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAll();
    }
}
