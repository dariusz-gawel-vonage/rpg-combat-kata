using RPGKataLogic.Models;

namespace RPGKataLogic.Logic;

public static class CharactersInteractionsService
{
    public static void Damage(Character attacker, Character target, int damage)
    {
        if (target.LiveState == LiveState.Dead ||
            attacker == target)
            return;

        damage = CalculateFinalDamage(attacker, target, damage);

        if (target.Health <= damage)
        {
            target.Health = 0;
            target.LiveState = LiveState.Dead;
        }
        else
        {
            target.Health -= damage;
        }
    }

    public static void Heal(Character healer, Character target, int healAmount)
    {
        if (target.LiveState == LiveState.Dead || 
            healer != target)
            return;

        target.Health = Math.Min(target.Health + healAmount, 1000);
    }

    private static int CalculateFinalDamage(Character attacker, Character target, int damage)
    {
        if (target.Level >= attacker.Level + 5)
            damage = (int)(damage * 0.5);
        else if (target.Level >= attacker.Level - 5)
            damage = (int)(damage * 1.5);
        return damage;
    }
}
