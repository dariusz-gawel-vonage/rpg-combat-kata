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
        var distance = _mapService.CalculateDistance(attacker, target);

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

        attacker.GainExperience(damage);
    }

    public void Heal(Character healer, Character target, int healAmount)
    {
        if (CanHeal(healer, target))
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
