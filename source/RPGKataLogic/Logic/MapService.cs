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
        var isAlreadyAtMap = _beingsLocationLookup.TryGetValue(being, out var currentGround);

        if(isAlreadyAtMap == false)
            _beingsLocationLookup.Add(being, null);
        else
            _map[currentGround.X, currentGround.Y].Beings.Remove(being);

        _beingsLocationLookup[being] = desiredGround;
        _map[desiredGround.X, desiredGround.Y].Beings.Add(being);

    }

    public void RemoveBeingFromMap(Being being)
    {
        _beingsLocationLookup.TryGetValue(being, out var ground);
        if (ground == null)
            throw new InvalidOperationException("There was no character at the map, while trying to remove it.");

        _beingsLocationLookup[being].Beings.Remove(being);
        _beingsLocationLookup.Remove(being);

        _map[ground.X, ground.Y].Beings.Remove(being);
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

    public double CalculateDistance(Being first, Being second)
    {
        var firstDistance = GetBeingLocation(first);
        var secondDistance = GetBeingLocation(second);

        if (firstDistance == null || secondDistance == null)
            throw new Exception("Both characters must be on the map to calculate distance.");

        return Math.Sqrt(Math.Pow(secondDistance.X - firstDistance.X, 2) + Math.Pow(secondDistance.Y - firstDistance.Y, 2));
    }

    public List<Being> GetBeingsInRange(Being being, int range, bool skipItself = true, bool skipOver = true)
    {
        var beingsInRange = new List<Being>();

        var location = GetBeingLocation(being);
        if (location == null) 
            return beingsInRange;

        for (int x = location.X - range; x <= location.X + range; x++)
        {
            for (int y = location.Y - range; y <= location.Y + range; y++)
            {
                if (IsOutsideTheMap(x, y) || IsOutsideTheRange(location, (x,y), range))
                    continue;

                var beingsAtLocation = _map[x, y].Beings;
                if(skipOver)
                    beingsAtLocation = beingsAtLocation.Where(b => !b.IsOver()).ToList();
                beingsInRange.AddRange(beingsAtLocation);
            }
        }

        if(skipItself)
            beingsInRange.Remove(being);

        return beingsInRange;
    }

    private bool IsOutsideTheMap(int x, int y)
    {
        return x < 0 || x >= _width || y < 0 || y >= _height;
    }

    private bool IsOutsideTheRange(Ground location, (int x, int y) target, int range)
    {
        return Math.Sqrt(Math.Pow(target.x - location.X, 2) + Math.Pow(target.y - location.Y, 2)) > range;
    }
}