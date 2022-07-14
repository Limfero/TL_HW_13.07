using CarProductionApp.Models;

namespace CarProductionApp.Repositories
{
    public interface ICarDealershipRepository
    {
        IReadOnlyList<CarDealership> GetAll();
    }
}
