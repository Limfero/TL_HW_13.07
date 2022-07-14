using CarProductionApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace CarProductionApp.Repositories
{
    public class RawSqlPurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly string _connectionString;

        public RawSqlPurchaseOrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IReadOnlyList<PurchaseOrder> GetAll()
        {
            var result = new List<PurchaseOrder>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [NameBuyer], [DealershipId], [CarId] from [PurchaseOrder]";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new PurchaseOrder(
                    Convert.ToString(reader["NameBuyer"]),
                    Convert.ToInt32(reader["DealershipId"]),
                    Convert.ToInt32(reader["CarId"])
                ));
            }

            return result;
        }
    }
}
