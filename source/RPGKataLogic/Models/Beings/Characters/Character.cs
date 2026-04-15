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
        LiveState = LiveState.Alive;
        Level = level;
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
        while (Experience % 1000 == 0)
        {
            Level++;
        }
    }
}
    