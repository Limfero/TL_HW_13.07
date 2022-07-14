using System;

namespace CarProductionApp.Models
{
    public class PurchaseOrder
    {
        public string NameBuyer { get; private set; }
        public int DealershipId { get; private set; }
        public int CarId { get; private set; }

        public PurchaseOrder(string nameBuyer, int dealershipId, int carId)
        {
            NameBuyer = nameBuyer;
            DealershipId = dealershipId;
            CarId = carId;
        }


    }
}
