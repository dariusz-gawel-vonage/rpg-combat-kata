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

    public void SetCharacterLocation(Being character, Ground desiredGround)
    {
        if (_charactersLocationLookup.TryGetValue(character, out var currentGround))
            _charactersLocationLookup.Add(character, null);

        _charactersLocationLookup[character] = desiredGround;
    }

    public void RemoveCharacterFromMap(Being character)
    {
        _charactersLocationLookup.TryGetValue(character, out var ground);
        if (ground == null)
            throw new InvalidOperationException("There was no character at the map, while trying to remove it.");

        _charactersLocationLookup[character].Beings.Remove(character);
        _charactersLocationLookup.Remove(character);
    }

    public Ground? GetCharacterLocation(Being character)
    {
        if (_charactersLocationLookup.TryGetValue(character, out var ground))
            return ground;

        return null;
    }
}