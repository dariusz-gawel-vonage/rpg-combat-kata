namespace RPGKataLogic.Models;

public abstract class Being
{
    public Guid Id { get; set; }
    public int Health { get; set; }

    public Being()
    {
        Id = Guid.NewGuid();
    }

    public Being(int health) : this()
    {
        Health = health;
    }

    public abstract void SetOver();
    public abstract bool IsOver();
}
