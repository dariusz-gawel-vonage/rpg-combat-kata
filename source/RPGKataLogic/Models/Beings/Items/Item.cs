using RPGKataLogic.Enums;

namespace RPGKataLogic.Models;

public class Item : WorldObject
{
    public Item(int health) : base(health)
    {
    }

    public override string ToString() => $"ITEM ID: {Id} HEALTH: {Durability}, STATE: {State}";
}
