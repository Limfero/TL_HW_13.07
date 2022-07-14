using CarProductionApp.Models;

namespace CarProductionApp.Repositories
{
    public interface IPurchaseOrderRepository
    {
        IReadOnlyList<PurchaseOrder> GetAll();
    }
}
