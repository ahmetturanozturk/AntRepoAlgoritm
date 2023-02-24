namespace AntRepo.Models;

public class TemporaryWarehouse
{
    public TemporaryWarehouse(decimal capacity, string name, int index)
    {
        Capacity = capacity;
        Name = name;
        Index = index;
    }

    public string Name { get; set; }
    public int Index { get; set; }
    public decimal Capacity { get; private set; } = 0;

    public void Unload()
    {
        Capacity = 0;
    }
    
    public void Load(Car car)
    {
        if(car.Unload());
        Capacity++;
    }

    public bool IsAvailable()
    {
        return Capacity < Constants.MaxCapacity;
    }
    

}