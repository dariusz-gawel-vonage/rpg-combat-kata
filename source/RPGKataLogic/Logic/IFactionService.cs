using RPGKataLogic.Models;

namespace RPGKataLogic.Logic
{
    public interface IFactionService
    {
        void AddFaction(string name);
        void RemoveFaction(string name);
        void UpdateFaction(string oldName, string newName);

        void JoinFaction(Faction faction, Character character);
        void JoinFaction(string factionName, Character character);
        void LeaveFaction(Faction faction, Character character);

        bool AreAllies(Character first, Character second);
        List<Faction> GetAllFactionsOfCharacter(Character character);
    }
}