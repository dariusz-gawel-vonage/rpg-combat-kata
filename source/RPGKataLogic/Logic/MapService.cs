using RPGKataLogic.Models;

namespace RPGKataLogic.Logic;

public class MapService : IMapService
{
    private Ground[,] _map;
    private Dictionary<Being, Ground> _charactersLocationLookup;

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
        _charactersLocationLookup = new Dictionary<Being, Ground>();
    }

    public Ground? GetCharacterLocation(Being character)
    {
        if (_charactersLocationLookup.TryGetValue(character, out var ground))
            return ground;

        return null;
    }
}