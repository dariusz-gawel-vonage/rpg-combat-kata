namespace RPGKataLogic.Models;
public class Character
{
    public int Health { get; set; }
    public int Level { get; set; }
    public LiveState LiveState { get; set; }

    public Character()
    {
        Health = 1000;
        Level = 1;
        LiveState = LiveState.Alive;
    }
    public Character(int health, int level)
    {
        Health = health;
        Level = level;
        LiveState = LiveState.Alive;
    }
}
    