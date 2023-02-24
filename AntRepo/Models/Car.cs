namespace AntRepo.Models;

public class Car
{
    public Car(decimal capacity, int index)
    {
        Capacity = capacity;
        Index = index;
    }

    public int Index { get; set; }
    public decimal Capacity { get; internal set; }
    
    public bool Unload()
    {
        if (Capacity > 0)
        {
            Capacity--;
            return true;
        }
        else
        {
            return false;
        }
    }
}