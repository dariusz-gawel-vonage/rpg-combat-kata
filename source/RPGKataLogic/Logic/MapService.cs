using RPGKataLogic.Models;

namespace RPGKataLogic.Logic;

public class MapService : IMapService
{
    private Ground[,] _map;
    private readonly int _width;
    private readonly int _height;
    private Dictionary<Being, Ground> _beingsLocationLookup;

    public MapService(int width, int height)
    {
        _width = width;
        _height = height;
        _map = new Ground[_width, _height];
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
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

    public void MoveBeingRandomWay(Being being)
    {
        var random = new Random();
        var currentGround = GetBeingLocation(being);
        switch(random.Next(4))
        {
            case 0: // Move up
                if (currentGround?.Y < _height - 1)
                    SetBeingLocation(being, _map[currentGround.X, currentGround.Y + 1]);
                else
                    MoveBeingRandomWay(being);
                break;
            case 1: // Move right
                if (currentGround?.X < _width - 1)
                    SetBeingLocation(being, _map[currentGround.X + 1, currentGround.Y]);
                else
                    MoveBeingRandomWay(being);
                break;
            case 2: // Move down
                if (currentGround?.Y > 0)
                    SetBeingLocation(being, _map[currentGround.X, currentGround.Y - 1]);
                else
                    MoveBeingRandomWay(being);
                break;
            case 3: // Move left
                if (currentGround?.X > 0)
                    SetBeingLocation(being, _map[currentGround.X - 1, currentGround.Y]);
                else
                    MoveBeingRandomWay(being);
                break;
        }
    }
}