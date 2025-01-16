using ChovEvid.Entities;
using ChovEvidApi.Dto;
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

        public IEnumerable<DogDto> GetAll()
        {
            var dogs = new List<DogDto>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT d.id, d.name, d.name || ' ' || chs.name as full_name, " +
                                            "p.first_name || ' ' || p.last_name as owner, sex, state " +
                                            "FROM chovevid_dog d " +
                                            "JOIN chovevid_person p on(p.id=d.id_owner) " +
                                            "JOIN chovevid_breeding_station chs on(chs.id=d.id_breeding_station) " +
                                            "ORDER BY d.id";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dogs.Add(new DogDto
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                FullName = reader.GetString(2),
                                Owner = reader.GetString(3),
                                Sex = reader.GetString(4),
                                State = reader.GetString(5)
                            });
                        }
                    }
                }
            }

            return dogs;
        }
    }
}
