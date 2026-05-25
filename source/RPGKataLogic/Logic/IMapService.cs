using RPGKataLogic.Models;

namespace RPGKataLogic.Logic
{
    public interface IMapService
    {
        void SetBeingLocation(WorldObject being, Ground desiredGround);
        void RemoveBeingFromMap(WorldObject being);
        void MoveBeingRandomWay(WorldObject being);
        Ground? GetGround(int x, int y);
        Ground? GetBeingLocation(WorldObject being);
        double CalculateDistance(WorldObject first, WorldObject second);
        List<WorldObject> GetBeingsInRange(WorldObject being, int range, bool skipItself = true, bool skipOver = true);
        void DisplayMap();
    }
}