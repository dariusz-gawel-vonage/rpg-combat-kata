using RPGKataLogic.Enums;

namespace RPGKataLogic.Models;

public class Item : Being
{
    public ConditionState Condition { get; set; }

    public Item(int health) : base(health)
    {
        Condition = ConditionState.Alive;
    }

    public override void SetOver()
    {
        Health = 0;
        Condition = ConditionState.Destroyed;
    }

    public override bool IsOver() => Condition == ConditionState.Destroyed;
}
