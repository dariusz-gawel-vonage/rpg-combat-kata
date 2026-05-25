using RPGKataLogic.Enums;

namespace RPGKataLogic.Models;

public abstract class WorldObject
{
    public Guid Id { get; private set; }
    public int Durability { get; protected set; }
    public ObjectState State { get; protected set; }

    public WorldObject()
    {
        Id = Guid.NewGuid();
        State = ObjectState.Active;
    }

    public WorldObject(int health) : this()
    {
        Durability = health;
    }

    public virtual void SetOver()
    {
        Durability = 0;
        State = ObjectState.Destroyed;
    }

    public bool IsOver() => State == ObjectState.Destroyed;

    public abstract override string ToString();

    internal void TakeDamage(int damage)
    {
        if (Durability <= damage)
            SetOver();
        else
            Durability -= damage;
    }
}
