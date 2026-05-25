using RPGKataLogic.Enums;

namespace RPGKataLogic.Models;

public class Character : WorldObject
{
    public int Experience { get; private set; }
    public int Level { get; private set; }

    public Character() : base(1000)
    {
        Experience = 0;
        Level = 1;
    }

    public Character(int health, int level) : base(health)
    {
        Experience = 0;
        Level = level;
    }

    public void GainExperience(int experience)
    {
        Experience += experience;
        Level = (int)Math.Floor(Math.Sqrt(Experience / 1000.0));
    }

    public override string ToString() => $"CHARACTER ID: {Id} HEALTH: {Durability}, LEVEL: {Level}, EXPERIENCE: {Experience}, STATE: {State}";

    internal int CalculateIncomingDamage(int damage, int attackerLevel)
    {
        if (Level >= attackerLevel + 5)
            return (int)(damage * 0.5);
        else if (Level <= attackerLevel - 5)
            return (int)(damage * 1.5);

        return damage;
    }

    internal void TakeHeal(int healAmount)
    {
        Durability = Math.Min(Durability + healAmount, 1000);
    }

    internal void SetLevel(int level)
    {
        var experienceNeeded = (int)Math.Pow(level, 2) * 1000;
        Experience = experienceNeeded;
        Level = level;
    }
}
