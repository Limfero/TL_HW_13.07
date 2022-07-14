using CarProductionApp.Models;

namespace CarProductionApp.Repositories
{
    public interface ICarRepository
    {
        IReadOnlyList<Car> GetAll();
        IReadOnlyList<Car> GetCarByManufacturerId(int manufacturerId);
        IReadOnlyList<Car> GroupFromCountOrder(int count);
        Car GetCarById(int carId);
        void UpdateCar(Car car);
        void DeleteCar(Car car);
    }
}
