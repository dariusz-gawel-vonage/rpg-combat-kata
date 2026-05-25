namespace RPGKataLogic.Models;

public abstract class WorldObject
{
    public Guid Id { get; private set; }
    public int Durability { get; protected set; }

    public WorldObject()
    {
        Id = Guid.NewGuid();
    }

    public WorldObject(int health) : this()
    {
        Durability = health;
    }

    public abstract void SetOver();
    public abstract bool IsOver();
    public abstract override string ToString();

    internal void TakeDamage(int damage)
    {
        if (Durability <= damage)
            SetOver();
        else
            Durability -= damage;
    }
}
