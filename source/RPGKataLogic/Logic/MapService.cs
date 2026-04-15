using RPGKataLogic.Models;

namespace RPGKataLogic.Logic;

public class MapService : IMapService
{
    private Ground[,] _map;
    private Dictionary<Being, Ground> _beingsLocationLookup;

    public MapService(int width, int height)
    {
        _map = new Ground[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                _map[i, j] = new Ground { X = i, Y = j, Beings = new List<Being>() };
            }
        }
        _beingsLocationLookup = new Dictionary<Being, Ground>();
    }

    public void SetBeingLocation(Being being, Ground desiredGround)
    {
        if (_beingsLocationLookup.TryGetValue(being, out var currentGround))
            _beingsLocationLookup.Add(being, null);

        _beingsLocationLookup[being] = desiredGround;
    }

    public void RemoveBeingFromMap(Being being)
    {
        _beingsLocationLookup.TryGetValue(being, out var ground);
        if (ground == null)
            throw new InvalidOperationException("There was no character at the map, while trying to remove it.");

        _beingsLocationLookup[being].Beings.Remove(being);
        _beingsLocationLookup.Remove(being);
    }

    public Ground? GetBeingLocation(Being being)
    {
        if (_beingsLocationLookup.TryGetValue(being, out var ground))
            return ground;

        return null;
    }
}