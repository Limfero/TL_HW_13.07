using System;

namespace CarProductionApp.Models
{
    public class Car
    {
        public int CarId { get; private set; }
        public string Model { get; private set; }
        public DateTime BuildData { get; private set; }
        public int Price { get; private set; }
        public int ManufacturerId { get; private set; }

        public Car(int carId, string model, DateTime buildData, int price, int manufacturerId)
        {
            CarId = carId;
            Model = model;
            BuildData = buildData;
            Price = price;
            ManufacturerId = manufacturerId;
        }

        public void UpdateModel(string newModel)
        {
            Model = newModel;
        }

        public void UpdateBuildData(DateTime newBuildData)
        {
            BuildData = newBuildData;
        }

        public void UpdatePrice(int newPrice)
        {
            Price = newPrice;
        }

        public void UpdateManufacturerId(int newManufacturerId)
        {
            ManufacturerId = newManufacturerId;
        }
    }
}
