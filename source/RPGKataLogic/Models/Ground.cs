using System.Collections.ObjectModel;

namespace RPGKataLogic.Models;

public class Ground
{
    public int X { get; set; }
    public int Y { get; set; }
    private List<WorldObject> _beings;

    public Ground()
    {
        _beings = new();
    }

    public void AddBeing(WorldObject being)
    {
        _beings.Add(being);
    }

    public void RemoveBeing(WorldObject being)
    {
        _beings.Remove(being);
    }

    public ReadOnlyCollection<WorldObject> GetBeings(bool skipOver = true)
    {
        if (skipOver)
            return _beings.Where(b => !b.IsOver()).ToList().AsReadOnly();
        
        return _beings.AsReadOnly();
    }
}