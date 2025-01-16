using ChovEvid.Entities;
using Oracle.ManagedDataAccess.Client;

namespace ChovEvid.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly string _connectionString;

        public DogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Dog> GetAll()
        {
            var dogs = new List<Dog>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT id, id_owner, id_breeding_station, name, state " +
                                            "FROM chovevid_dog ORDER BY id";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dogs.Add(new Dog
                            {
                                Id = reader.GetInt32(0),
                                IdOwner = reader.GetInt32(1),
                                IdBreedingStation = reader.GetString(2),
                                Name = reader.GetString(3),
                                State = reader.GetString(4)
                            });
                        }
                    }
                }
            }

            return dogs;
        }
    }
}
