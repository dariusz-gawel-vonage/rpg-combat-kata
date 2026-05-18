using System.Collections.ObjectModel;

namespace RPGKataLogic.Models;

public class Ground
{
    public int X { get; set; }
    public int Y { get; set; }
    private List<Being> _beings;

    public Ground()
    {
        _beings = new();
    }

    public void AddBeing(Being being)
    {
        _beings.Add(being);
    }

    public void RemoveBeing(Being being)
    {
        _beings.Remove(being);
    }

    public ReadOnlyCollection<Being> GetBeings(bool skipOver = true)
    {
        if (skipOver)
            return _beings.Where(b => !b.IsOver()).ToList().AsReadOnly();
        
        return _beings.AsReadOnly();
    }
}