using RPGKataLogic.Models;

namespace RPGKataLogic.Logic
{
    public interface IMapService
    {
        Ground GetCharacterLocation(Character character);
    }
}