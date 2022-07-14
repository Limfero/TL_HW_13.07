using CarProductionApp.Models;
using System.Data.SqlClient;

namespace CarProductionApp.Repositories
{
    public class RawSqlManufacturerRepository: IManufacturerRepository
    {
        private readonly string _connectionString;

        public RawSqlManufacturerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IReadOnlyList<Manufacturer> GetAll()
        {
            var result = new List<Manufacturer>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [ManufacturerId], [NameFactory], [Headquarters], [FoundationDate] from [Manufacturer]";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Manufacturer(
                    Convert.ToInt32(reader["ManufacturerId"]),
                    Convert.ToString(reader["NameFactory"]),
                    Convert.ToString(reader["Headquarters"]),
                    Convert.ToDateTime(reader["FoundationDate"])
                ));
            }

            return result;
        }


    }
}
