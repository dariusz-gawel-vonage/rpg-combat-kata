using RPGKataLogic.Enums;

namespace RPGKataLogic.Models;

public class Item : WorldObject
{
    public ConditionState Condition { get; private set; }

    public Item(int health) : base(health)
    {
        Condition = ConditionState.Alive;
    }

    public override void SetOver()
    {
        Durability = 0;
        Condition = ConditionState.Destroyed;
    }

    public override bool IsOver() => Condition == ConditionState.Destroyed;

    public override string ToString() => $"ITEM ID: {Id} HEALTH: {Durability}, CONDITION: {Condition}";
}
