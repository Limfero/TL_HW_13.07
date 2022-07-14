using CarProductionApp.Models;

namespace CarProductionApp.Repositories
{
    public interface IManufacturerRepository
    {
        IReadOnlyList<Manufacturer> GetAll();
    }
}
