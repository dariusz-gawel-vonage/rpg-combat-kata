using RPGKataLogic.Models;

namespace RPGKataLogic.Logic;

public class CharactersInteractionsService
{
    private IMapService _mapService;

    public CharactersInteractionsService(IMapService mapService)
    {
        _mapService = mapService;
    }

    public void Damage(Fighter attacker, Fighter target, int damage)
    {
        var distance = CalculateDistance(attacker, target);

        if (IsDamagePossible(attacker, target, distance))
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

    private double CalculateDistance(Fighter attacker, Fighter target)
    {
        var attackerDistance = _mapService.GetCharacterLocation(attacker);
        var targetDistance = _mapService.GetCharacterLocation(target);

        if(attackerDistance == null || targetDistance == null)
            throw new Exception("Both characters must be on the map to calculate distance.");

        return Math.Sqrt(Math.Pow(targetDistance.X - attackerDistance.X, 2) + Math.Pow(targetDistance.Y - attackerDistance.Y, 2));
    }

    private static bool IsDamagePossible(Fighter attacker, Fighter target, double distance)
    {
        if (target.LiveState == LiveState.Dead)
            return false;

        if (attacker == target)
            return false;
        
        if(distance > attacker.GetAttackRange())
            return false;

        return true;
    }
}
