using RPGKataLogic.Enums;

namespace RPGKataLogic.Models;

public class Fighter : Character
{
    public FighterType FighterType { get; set; }

    public Fighter(int health, int level, FighterType fighterType) : base(health, level)
    {
        FighterType = fighterType;
    }

    public int GetAttackRange()
    {
        return FighterType switch
        {
            FighterType.Melee => 2,
            FighterType.Ranged => 20,
            _ => throw new Exception("Unknown fighter type")
        };
    }
}
