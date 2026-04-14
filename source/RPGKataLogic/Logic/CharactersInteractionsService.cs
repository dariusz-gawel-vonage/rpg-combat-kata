using RPGKataLogic.Models;

namespace RPGKataLogic.Logic;

public static class CharactersInteractionsService
{
    public static void Damage(Character target, int damage)
    {
        if(target.LiveState == LiveState.Dead)
            return;

        if(target.Health <= damage)
        {
            target.Health = 0;
            target.LiveState = LiveState.Dead;
        }
        else
        {
            target.Health -= damage;
        }
    }

    public static void Heal(Character target, int healAmount)
    {
        if (target.LiveState == LiveState.Dead)
            return;

        target.Health = Math.Min(target.Health + healAmount, 1000);
    }
}
