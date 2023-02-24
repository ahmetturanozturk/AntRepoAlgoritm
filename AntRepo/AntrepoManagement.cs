using AntRepo.Models;

namespace AntRepo;

public static class AntrepoManagement
{
    public static string AntrepoYerlestir(string depoDurum, string[] gelenVagonlar)
    {
        List<TemporaryWarehouse> warehouses = ParseWarehouse(depoDurum);
        List<List<Car>> carsList = ParseCars(gelenVagonlar);

        foreach (var cars in carsList)
        {
            while (cars.CarControl())
            {
                Car car = cars.First(s => s.Capacity > 0);
                TemporaryWarehouse warehouse = warehouses.GetAvailableWarehouse(car.Index);

                warehouse.Load(car);
                Console.WriteLine($"Yükleme s. Car = Capacity; {car.Capacity}, I; {car.Index} WareHouse= Capacity; {warehouse.Capacity} I; {warehouse.Index}");
            }
        }

        string result = String.Join("", warehouses.Select(s => s.Capacity).ToArray().Reverse());
        result = result.Insert(1, "#");
        return result;
    }
    

    private static List<TemporaryWarehouse> ParseWarehouse(string depoDurum)
    {
        List<TemporaryWarehouse> warehouses = new List<TemporaryWarehouse>();
        int letterIndex = 1, index = 1, letter = 65;
        foreach (var depo in depoDurum.Split("#")[1].Reverse())
        {
            //isimlendirme için
            if (letterIndex == 6)
            {
                letterIndex = 1;
                letter++;
            }            
            TemporaryWarehouse temporaryWarehouse = new TemporaryWarehouse(int.Parse(depo.ToString()), $"{(char)letter}{letterIndex}" , index);
            warehouses.Add(temporaryWarehouse);
            letterIndex++;
            index++;
        }
        warehouses.Add(new TemporaryWarehouse(int.Parse(depoDurum.Split("#")[0].ToString()), Constants.UnLimitedWareHouse, 30));
        return warehouses;
    }

    private static List<List<Car>> ParseCars(string[] gelenVagonlar)
    {
        List<List<Car>> allCars = new List<List<Car>>();
        foreach (var cars in gelenVagonlar)
        {
            List<Car> carList = new List<Car>();
            int index = 1;
            foreach (var carCapacity in cars.Reverse())
            {
                Car car = new Car(int.Parse(carCapacity.ToString()),index);
                carList.Add(car);
                index++;
            }
            allCars.Add(carList);
        }

        return allCars;
    }

    private static bool CarControl(this List<Car> cars)
    {
        return cars.Sum(s => s.Capacity)>0; //Dolu olan car var
    }

    private static TemporaryWarehouse GetAvailableWarehouse(this List<TemporaryWarehouse> warehouses, int Index)
    {
        TemporaryWarehouse warehouse = warehouses.First(f=>  f.Index >= Index);
        
        while (!warehouse.IsAvailable() && warehouse.Name != "F5")
        {
            warehouse.Unload();
            TemporaryWarehouse newWareHouse = warehouses.GetAvailableWarehouse( warehouse.Index+1);

            return newWareHouse;
        }
        return warehouse;
    }
}