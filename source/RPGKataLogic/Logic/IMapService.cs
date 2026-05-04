using RPGKataLogic.Models;

namespace RPGKataLogic.Logic
{
    public interface IMapService
    {
        void SetBeingLocation(Being being, Ground desiredGround);
        void RemoveBeingFromMap(Being being);
        void MoveBeingRandomWay(Being being);
        Ground? GetGround(int x, int y);
        Ground? GetBeingLocation(Being being);
        double CalculateDistance(Being first, Being second);
        List<Being> GetBeingsInRange(Being being, int range, bool skipItself = true, bool skipOver = true);
        void DisplayMap();
    }
}