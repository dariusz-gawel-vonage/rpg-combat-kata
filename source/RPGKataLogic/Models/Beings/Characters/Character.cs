using RPGKataLogic.Enums;

namespace RPGKataLogic.Models;

public class Character : Being
{
    public int Experience { get; set; }
    public int Level { get; set; }
    public LiveState LiveState { get; set; }

    public Character() : base(1000)
    {
        Experience = 0;
        Level = 1;
        LiveState = LiveState.Alive;
    }

    public Character(int health, int level) : base(health)
    {
        Experience = 0;
        Level = level;
        LiveState = LiveState.Alive;
    }

    public override void SetOver()
    {
        Health = 0;
        LiveState = LiveState.Dead;
    }

    public override bool IsOver() => LiveState == LiveState.Dead;

    public void GainExperience(int experience)
    {
        Experience += experience;
        Level = (int)Math.Floor(Math.Sqrt(Experience / 1000.0));
    }

    public override string ToString() => $"CHARACTER ID: {Id} HEALTH: {Health}, LEVEL: {Level}, EXPERIENCE: {Experience}, STATE: {LiveState}";

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
        Health = Math.Min(Health + healAmount, 1000);
    }

    internal void SetLevel(int level)
    {
        var experienceNeeded = (int)Math.Pow(level, 2) * 1000;
        Experience = experienceNeeded;
        Level = level;
    }
}
