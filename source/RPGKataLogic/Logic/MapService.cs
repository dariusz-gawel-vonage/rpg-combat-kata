using RPGKataLogic.Models;

namespace RPGKataLogic.Logic;

public class MapService : IMapService
{
    private Ground[,] _map;
    private Dictionary<Character, Ground> _charactersLocationLookup;

    public MapService(int width, int height)
    {
        _map = new Ground[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                _map[i, j] = new Ground { X = i, Y = j, Characters = new List<Character>() };
            }
        }
        _charactersLocationLookup = new Dictionary<Character, Ground>();
    }

    public Ground GetCharacterLocation(Character character)
    {
        if (_charactersLocationLookup.TryGetValue(character, out var ground))
            return ground;
        return null;
    }
}

public class Ground
{
    public int X { get; set; }
    public int Y { get; set; }
    public List<Character> Characters { get; set; }
}