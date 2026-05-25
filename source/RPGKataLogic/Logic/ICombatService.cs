using RPGKataLogic.Models;

namespace RPGKataLogic.Logic
{
    public interface ICombatService
    {
        void Damage(Fighter attacker, WorldObject target, int damage);
        void Heal(Character healer, Character target, int healAmount);
    }
}