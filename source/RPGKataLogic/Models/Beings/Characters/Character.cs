using RPGKataLogic.Enums;

namespace RPGKataLogic.Models;

public class Character : Being
{
    public int Level { get; set; }
    public LiveState LiveState { get; set; }

    public Character() : base(1000)
    {
        LiveState = LiveState.Alive;
        Level = 1;
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
}
    