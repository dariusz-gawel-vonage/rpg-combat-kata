using RPGKataLogic.Models;

namespace RPGKataLogic.Logic
{
    public interface ICombatService
    {
        void Damage(Fighter attacker, Being target, int damage);
        void Heal(Character healer, Character target, int healAmount);
    }
}