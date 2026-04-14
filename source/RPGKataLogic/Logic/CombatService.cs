using RPGKataLogic.Models;

namespace RPGKataLogic.Logic;

public class CombatService
{
    private IMapService _mapService;
    private IFactionService _factionService;

    public CombatService(IMapService mapService, IFactionService factionService)
    {
        _mapService = mapService;
        _factionService = factionService;
    }

    public void Damage(Fighter attacker, Character target, int damage)
    {
        var distance = CalculateDistance(attacker, target);

        if (CanDamage(attacker, target, distance))
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

    public void Heal(Character healer, Character target, int healAmount)
    {
        if (CanHeal(healer, target))
            return;

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

    private double CalculateDistance(Character attacker, Character target)
    {
        var attackerDistance = _mapService.GetCharacterLocation(attacker);
        var targetDistance = _mapService.GetCharacterLocation(target);

        if(attackerDistance == null || targetDistance == null)
            throw new Exception("Both characters must be on the map to calculate distance.");

        return Math.Sqrt(Math.Pow(targetDistance.X - attackerDistance.X, 2) + Math.Pow(targetDistance.Y - attackerDistance.Y, 2));
    }

    private bool CanDamage(Fighter attacker, Character target, double distance)
    {
        if (target.LiveState == LiveState.Dead)
            return false;

        if (attacker == target)
            return false;
        
        if(distance > attacker.GetAttackRange())
            return false;

        if (_factionService.AreAllies(attacker, target))
            return false;

        return true;
    }

    private bool CanHeal(Character healer, Character target)
    {
        if (target.LiveState == LiveState.Dead)
            return false;

        if (_factionService.AreAllies(healer, target) == false)
            return false;

        if (healer != target)
            return false;

        return true;
    }
}
