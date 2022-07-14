using CarProductionApp.Models;
using System.Data.SqlClient;

namespace CarProductionApp.Repositories
{
    public class RawSqlCarDealershipRepository: ICarDealershipRepository
    {
        private readonly string _connectionString;

        public RawSqlCarDealershipRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IReadOnlyList<CarDealership> GetAll()
        {
            var result = new List<CarDealership>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [DealershipId], [NameDealership], [Supplier] from [CarDealership]";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new CarDealership(
                    Convert.ToInt32(reader["DealershipId"]),
                    Convert.ToString(reader["NameDealership"]),
                    Convert.ToInt32(reader["Supplier"])
                ));
            }

            return result;
        }


    }
}
