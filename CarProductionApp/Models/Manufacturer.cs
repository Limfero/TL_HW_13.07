using System;

namespace CarProductionApp.Models
{
    public class Manufacturer
    {
        public int ManufacturerId { get; private set; }
        public string NameFactory { get; private set; }
        public string Headquarters { get; private set; }
        public DateTime FoundationDate { get; private set; }

        public Manufacturer(int manufacturerId, string nameFactory, string headquarters, DateTime foundationDate)
        {
            ManufacturerId = manufacturerId;
            NameFactory = nameFactory;
            Headquarters = headquarters;
            FoundationDate = foundationDate;
        }
    }
}
