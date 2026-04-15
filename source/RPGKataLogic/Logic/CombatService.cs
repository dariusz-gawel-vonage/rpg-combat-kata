using RPGKataLogic.Enums;
using RPGKataLogic.Models;

namespace RPGKataLogic.Logic;

public class CombatService : ICombatService
{
    private IMapService _mapService;
    private IFactionService _factionService;

    public CombatService(IMapService mapService, IFactionService factionService)
    {
        _mapService = mapService;
        _factionService = factionService;
    }

    public void Damage(Fighter attacker, Being target, int damage)
    {
        var distance = CalculateDistance(attacker, target);

        if (CanDamage(attacker, target, distance) == false)
            return;

        damage = CalculateFinalDamage(attacker, target, damage);

        if (target.Health <= damage)
        {
            target.SetOver();
            _mapService.RemoveBeingFromMap(target);
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



    private static int CalculateFinalDamage(Fighter attacker, Being target, int damage)
    {
        if (target is Character character)
        {
            if (character.Level >= attacker.Level + 5)
                damage = (int)(damage * 0.5);
            else if (character.Level >= attacker.Level - 5)
                damage = (int)(damage * 1.5);
            return damage;
        }

        return damage;
    }

    private double CalculateDistance(Being attacker, Being target)
    {
        var attackerDistance = _mapService.GetBeingLocation(attacker);
        var targetDistance = _mapService.GetBeingLocation(target);
        
        if (attackerDistance == null || targetDistance == null)
            throw new Exception("Both characters must be on the map to calculate distance.");

        return Math.Sqrt(Math.Pow(targetDistance.X - attackerDistance.X, 2) + Math.Pow(targetDistance.Y - attackerDistance.Y, 2));
    }

    private bool CanDamage(Fighter attacker, Being target, double distance)
    {
        if (target.IsOver())
            return false;

        if (attacker == target)
            return false;

        if (distance > attacker.GetAttackRange())
            return false;

        if (target is Character character && _factionService.AreAllies(attacker, character))
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
