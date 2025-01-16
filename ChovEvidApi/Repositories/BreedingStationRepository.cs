using ChovEvid.Entities;
using ChovEvidApi.Dto;
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

        public IEnumerable<BreedingStationDto> GetAll()
        {
            var breedingStations = new List<BreedingStationDto>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT chs.reg_number, chs.name, p.first_name || ' ' || p.last_name, count(d.id) as dog_count, chs.created, chs.location " +
                                            "FROM chovevid_breeding_station chs " +
                                            "JOIN chovevid_person p on(p.id=chs.id_owner) " +
                                            "JOIN chovevid_dog d on(d.id_breeding_station = chs.id) " +
                                            "GROUP BY chs.reg_number, chs.name, p.first_name || ' ' || p.last_name, chs.created, chs.location " +
                                            "ORDER BY chs.reg_number";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            breedingStations.Add(new BreedingStationDto
                            {
                                RegNumber = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Owner = reader.GetString(2),
                                DogCount = reader.GetInt32(3),
                                Created = reader.GetDateTime(4),
                                Location = reader.GetString(5)
                            });
                        }
                    }
                }
            }

            return breedingStations;
        }
    }
}
