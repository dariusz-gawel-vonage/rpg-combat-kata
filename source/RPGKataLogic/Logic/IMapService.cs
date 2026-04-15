using RPGKataLogic.Models;

namespace RPGKataLogic.Logic
{
    public interface IMapService
    {
        void SetBeingLocation(Being being, Ground desiredGround);
        void RemoveBeingFromMap(Being being);
        Ground? GetBeingLocation(Being being);
    }
}