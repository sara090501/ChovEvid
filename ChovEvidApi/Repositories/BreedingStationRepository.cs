using ChovEvid.Entities;
using Oracle.ManagedDataAccess.Client;

namespace ChovEvid.Repositories
{
    public class BreedingStationRepository : IBreedingStationRepository
    {
        private readonly string _connectionString;

        public BreedingStationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<BreedingStation> GetAll()
        {
            var breedingStations = new List<BreedingStation>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT id, id_owner, name, reg_number " +
                                            "FROM chovevid_breeding_station ORDER BY id";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            breedingStations.Add(new BreedingStation
                            {
                                Id = reader.GetInt32(0),
                                IdOwner = reader.GetInt32(1),
                                Name = reader.GetString(2),
                                RegNumber = reader.GetInt32(3)
                            });
                        }
                    }
                }
            }

            return breedingStations;
        }
    }
}
