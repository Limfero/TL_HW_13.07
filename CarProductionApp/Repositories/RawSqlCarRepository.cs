using CarProductionApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace CarProductionApp.Repositories
{
    public class RawSqlCarRepository: ICarRepository
    {
        private readonly string _connectionString;

        public RawSqlCarRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IReadOnlyList<Car> GetAll()
        {
            var result = new List<Car>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [CarId], [Model], [BuildData], [Price], [ManufacturerId] from [Car]";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Car(
                    Convert.ToInt32(reader["CarId"]),
                    Convert.ToString(reader["Model"]),
                    Convert.ToDateTime(reader["BuildData"]),
                    Convert.ToInt32(reader["Price"]),
                    Convert.ToInt32(reader["ManufacturerId"])
                ));
            }

            return result;
        }

        public IReadOnlyList<Car> GetCarByManufacturerId(int manufacturerId)
        {
            var result = new List<Car>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [CarId], [Model], [BuildData], [Price], [ManufacturerId] from [Car] where [ManufacturerId] = @manufacturerId";
            sqlCommand.Parameters.Add("@manufacturerId", SqlDbType.Int).Value = manufacturerId;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Car(
                    Convert.ToInt32(reader["CarId"]),
                    Convert.ToString(reader["Model"]),
                    Convert.ToDateTime(reader["BuildData"]),
                    Convert.ToInt32(reader["Price"]),
                    Convert.ToInt32(reader["ManufacturerId"])
                ));
            }

            return result;
        }

        public Car GetCarById(int carId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [CarId], [Model], [BuildData], [Price], [ManufacturerId] from [Car] where [CarId] = @carId";
            sqlCommand.Parameters.Add("@carId", SqlDbType.Int).Value = carId;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                return new Car(
                    Convert.ToInt32(reader["CarId"]),
                    Convert.ToString(reader["Model"]),
                    Convert.ToDateTime(reader["BuildData"]),
                    Convert.ToInt32(reader["Price"]),
                    Convert.ToInt32(reader["ManufacturerId"]
                ));
            }
            else
            {
                return null;
            }
        }

        public void UpdateCar(Car car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(nameof(car));
            }

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "update [Car] set [Model] = @model, [BuildData] = @buildData, [Price] = @price, [ManufacturerId] = @manufacturerId where [CarId] = @carId";
            sqlCommand.Parameters.Add("@carId", SqlDbType.Int).Value = car.CarId;
            sqlCommand.Parameters.Add("@model", SqlDbType.NVarChar, 100).Value = car.Model;
            sqlCommand.Parameters.Add("@buildData", SqlDbType.DateTime).Value = car.BuildData;
            sqlCommand.Parameters.Add("@price", SqlDbType.Int).Value = car.Price;
            sqlCommand.Parameters.Add("@manufacturerId", SqlDbType.Int).Value = car.ManufacturerId;
            sqlCommand.ExecuteNonQuery();

        }

        public void DeleteCar(Car car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(nameof(car));
            }

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "delete [Car] where [CarId] = @carId";
            sqlCommand.Parameters.Add("@carId", SqlDbType.Int).Value = car.CarId;
            sqlCommand.ExecuteNonQuery();
        }

        public IReadOnlyList<Car> GroupFromCountOrder(int count)
        {
            var result = new List<Car>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select * from [Car] where [CarId] in ( select [CarId] from [PurchaseOrder] group by [CarId] having count(*) >= @count )";
            sqlCommand.Parameters.Add("@count", SqlDbType.Int).Value = count;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Car(
                    Convert.ToInt32(reader["CarId"]),
                    Convert.ToString(reader["Model"]),
                    Convert.ToDateTime(reader["BuildData"]),
                    Convert.ToInt32(reader["Price"]),
                    Convert.ToInt32(reader["ManufacturerId"])
                ));
            }

            return result;
        }
    }
}
