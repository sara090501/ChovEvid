using ChovEvid.Entities;
using Oracle.ManagedDataAccess.Client;

namespace ChovEvid.Repositories
{
    internal class PersonRepository
    {
        private readonly string _connectionString;

        public PersonRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Person> GetAll()
        {
            var studyPrograms = new List<Person>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT id, first_name, last_name, phone_number, email " +
                                            "FROM chovevid_person ORDER BY id";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            studyPrograms.Add(new Person
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                PhoneNumber = reader.GetString(3),
                                Email = reader.GetString(4)
                            });
                        }
                    }
                }
            }

            return studyPrograms;
        }
    }
}
