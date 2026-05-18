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

        target.TakeDamage(damage);
        if(target.IsOver())
            _mapService.RemoveBeingFromMap(target);

        attacker.GainExperience(damage);
    }

    public void Heal(Character healer, Character target, int healAmount)
    {
        if (CanHeal(healer, target) == false)
            return;

        target.TakeHeal(healAmount);
    }

    private static int CalculateFinalDamage(Fighter attacker, Being target, int damage)
    {
        if (target is Character character)
            return character.CalculateIncomingDamage(damage, attacker.Level);

        return damage;
    }

    private bool CanDamage(Fighter attacker, Being target, double distance)
    {
        if (attacker.IsOver() || target.IsOver())
            return false;

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
        if (target.IsOver())
            return false;

        if (_factionService.AreAllies(healer, target) == false)
            return false;

        if (healer == target)
            return false;

        return true;
    }
}
