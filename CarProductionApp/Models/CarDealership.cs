using System;

namespace CarProductionApp.Models
{
    public class CarDealership
    {
        public int DealershipId { get; private set; }
        public string NameDealership { get; private set; }
        public int Supplier { get; private set; }

        public CarDealership(int dealershipId, string nameDealership, int supplier)
        {
            DealershipId = dealershipId;
            NameDealership = nameDealership;
            Supplier = supplier;
        }
    }
}
