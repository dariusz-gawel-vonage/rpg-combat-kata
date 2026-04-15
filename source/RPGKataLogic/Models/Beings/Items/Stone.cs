namespace RPGKataLogic.Models;

public class Stone : Item
{
    public Stone() : base(10000) { }

    public Stone(int health) : base(health) { }

    public override string ToString() => $"STONE ID: {Id} HEALTH: {Health}, CONDITION: {Condition}";
}
