namespace RPGKataLogic.Models;

public class Tree : Item
{
    public Tree() : base(2000) { }

    public Tree(int health) : base(health) { }

    public override string ToString() => $"TREE ID: {Id} HEALTH: {Health}, CONDITION: {Condition}";
}
