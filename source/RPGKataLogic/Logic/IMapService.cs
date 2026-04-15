using RPGKataLogic.Models;

namespace RPGKataLogic.Logic
{
    public interface IMapService
    {
        void SetCharacterLocation(Being character, Ground desiredGround);
        void RemoveCharacterFromMap(Being character);
        Ground? GetCharacterLocation(Being being);
    }
}