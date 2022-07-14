using CarProductionApp.Models;
using CarProductionApp.Repositories;

const string connectionString = @"Data Source=DESKTOP-B3FAFAI\SQLEXPRESS01;Initial Catalog=CarProduction;Pooling=true;Integrated Security=SSPI;TrustServerCertificate=True";

IManufacturerRepository manufacturerRepository = new RawSqlManufacturerRepository(connectionString);
ICarRepository carRepository = new RawSqlCarRepository(connectionString);
ICarDealershipRepository carDealershipRepository = new RawSqlCarDealershipRepository(connectionString);
IPurchaseOrderRepository purchaseOrderRepository = new RawSqlPurchaseOrderRepository(connectionString);

PrintCommands();
while (true)
{
    Console.WriteLine("Введите команду:");
    string command = Console.ReadLine();

    if ( command == "get-manufacturer" )
    {
        IReadOnlyList<Manufacturer> manufacturers = manufacturerRepository.GetAll();
        if (manufacturers.Count == 0)
        {
            Console.WriteLine("Производители не найдены!");
            continue;
        }

        foreach (Manufacturer manufacturer in manufacturers)
        {
            Console.WriteLine($"ManufacturerId: {manufacturer.ManufacturerId}, NameFactory: {manufacturer.NameFactory}, Headquarters: {manufacturer.Headquarters}, FoundationDate: {manufacturer.FoundationDate}");
        }
    }
    else if (command == "get-car")
    {
        IReadOnlyList<Car> cars = carRepository.GetAll();
        if (cars.Count == 0)
        {
            Console.WriteLine("Машины не найдены!");
            continue;
        }

        foreach (Car car in cars)
        {
            Console.WriteLine($"CarId: {car.CarId}, Model: {car.Model}, BuildData: {car.BuildData}, Price: {car.Price}, ManufacturerId: {car.ManufacturerId}");
        }
    }
    else if (command == "get-car-by-manufacturerid")
    {
        Console.WriteLine("Введите ID производителя: ");
        if (int.TryParse(Console.ReadLine(), out int manufacturerId))
        {
            IReadOnlyList<Car> cars = carRepository.GetCarByManufacturerId(manufacturerId);
            if (cars.Count == 0)
            {
                Console.WriteLine("Машины не найдены!");
                continue;
            }

            foreach (Car car in cars)
            {
                Console.WriteLine($"CarId: {car.CarId}, Model: {car.Model}, BuildData: {car.BuildData}, Price: {car.Price}, ManufacturerId: {car.ManufacturerId}");
            }
        }
        else
        {
            Console.WriteLine("Неверный ID производителя!");
        }
    }
    else if (command == "update-car-by-id")
    {
        Console.WriteLine("Введите ID автомобиля: ");
        if (int.TryParse(Console.ReadLine(), out int carId))
        {
            Car car = carRepository.GetCarById(carId);
            if (car == null)
            {
                Console.WriteLine($"Автомобиль по ID: {nameof(carId)} не найден");
            }

            Console.WriteLine("Введите новое название модели: ");
            string newModel = Console.ReadLine();
            if (string.IsNullOrEmpty(newModel))
            {
                Console.WriteLine("Неккорктное название модели");
                continue;
            }
            car.UpdateModel(newModel);

            Console.WriteLine("Введите новую дату сборк автомобиля: ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime newBuildData))
            {
                Console.WriteLine("Неверная дата!");
                continue;
            }
            car.UpdateBuildData(newBuildData);

            IReadOnlyList<Manufacturer> manufacturers = manufacturerRepository.GetAll();
            List<int> idManufacturers = new();
            foreach (Manufacturer manufacturer in manufacturers)
            {
                idManufacturers.Add(manufacturer.ManufacturerId);
            }

            Console.WriteLine("Введите новый ID производителя: ");
            if (!int.TryParse(Console.ReadLine(), out int newIdManufacturer) || !idManufacturers.Contains(newIdManufacturer))
            {
                Console.WriteLine("Неверный ID производителя!");
                continue;
            }
            car.UpdateManufacturerId(newIdManufacturer);

            carRepository.UpdateCar(car);
            Console.WriteLine("Автомобиль обновлен!");
        }
        else
        {
            Console.WriteLine("Неверный ID автомобля!");
        }

    }
    else if (command == "delete-car-by-id")
    {
        Console.WriteLine("Введите id автомобиля: ");
        if (int.TryParse(Console.ReadLine(), out int carId))
        {
            Car car = carRepository.GetCarById(carId);
            if (car == null)
            {
                Console.WriteLine($"Автомобиль по ID: {nameof(carId)} не найден");
            }

            carRepository.DeleteCar(car);
            Console.WriteLine("Автомобиль удален!");
        }
        else
        {
            Console.WriteLine("Неверный ID автомобиля!");
        }
    }
    else if (command == "group-car-from-count-order")
    {
        Console.WriteLine("Введите кол-во заказов: ");
        if (!int.TryParse(Console.ReadLine(), out int count))
        {
            Console.WriteLine("Неверное кол-во заказов!");
        }

        IReadOnlyList<Car> cars = carRepository.GroupFromCountOrder(count);
        if (cars.Count == 0)
        {
            Console.WriteLine("По такому кол-ву заказов машины не найдены!");
            continue;
        }

        foreach (Car car in cars)
        {
            Console.WriteLine($"CarId: {car.CarId}, Model: {car.Model}, BuildData: {car.BuildData}, Price: {car.Price}, ManufacturerId: {car.ManufacturerId}");
        }
    }
    else if (command == "get-cardealership")
    {
        IReadOnlyList<CarDealership> carDealerships = carDealershipRepository.GetAll();
        if (carDealerships.Count == 0)
        {
            Console.WriteLine("Дилеры не найдены!");
            continue;
        }

        foreach (CarDealership carDealership in carDealerships)
        {
            Console.WriteLine($"DealershipId: {carDealership.DealershipId}, NameDealership: {carDealership.NameDealership}, Supplier: {carDealership.Supplier}");
        }
    }
    else if (command == "get-purchaseorder")
    {
        IReadOnlyList<PurchaseOrder> purchaseOrders = purchaseOrderRepository.GetAll();
        if (purchaseOrders.Count == 0)
        {
            Console.WriteLine("Заказы не найдены!");
            continue;
        }

        foreach (PurchaseOrder purchaseOrder in purchaseOrders)
        {
            Console.WriteLine($"NameBuyer: {purchaseOrder.NameBuyer}, DealershipId: {purchaseOrder.DealershipId}, CarId: {purchaseOrder.CarId}");
        }
    }
    else if ( command == "help" )
    {
        PrintCommands();
    }
    else if ( command == "exit" )
    {
        break;
    }
    else
    {
        Console.WriteLine( "Неправильно введенная команда" );
    }
}

static void PrintCommands()
{
    Console.WriteLine( "Доступные комманды: " );

    Console.WriteLine( "get-manufacturer - получить список всех производителей" );

    Console.WriteLine( "get-car - получить список всех автомобилей" );
    Console.WriteLine( "get-car-by-manufacturerid - получить список машин по номеру производителя" );
    Console.WriteLine( "update-car-by-id - обновить автомобиль по id" );
    Console.WriteLine( "delete-car-by-id - удалить автомобиль по id" );
    Console.WriteLine( "group-car-from-count-order - объединить автомобили по колличеству заказов");

    Console.WriteLine( "get-cardealership - получить список всех дилеров" );

    Console.WriteLine( "get-purchaseorder - получить список всех заказов" );

    Console.WriteLine( "help - список команд" );
    Console.WriteLine( "exit - выход" );
}