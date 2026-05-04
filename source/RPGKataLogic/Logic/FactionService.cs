using RPGKataLogic.Models;

namespace RPGKataLogic.Logic;

public class FactionService : IFactionService
{
    private List<Faction> _factions;

    public FactionService()
    {
        _factions = new List<Faction>();
    }

    public void AddFaction(string name)
    {
        if (_factions.Any(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Faction with the same name already exists.");

        _factions.Add(new Faction(name));
    }

    public void RemoveFaction(string name)
    {
        var faction = _factions.FirstOrDefault(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (faction == null)
            throw new InvalidOperationException("Faction not found.");

        faction.Members.Clear();
        _factions.Remove(faction);
    }

    public void UpdateFaction(string oldName, string newName)
    {
        var faction = _factions.FirstOrDefault(f => f.Name.Equals(oldName, StringComparison.OrdinalIgnoreCase));
        if (faction == null)
            throw new InvalidOperationException("Faction not found.");

        if (_factions.Any(f => f.Name.Equals(newName, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Faction with the same name already exists.");

        faction.Name = newName;
    }



    public void JoinFaction(string factionName, Character character)
    {
        var faction = _factions.FirstOrDefault(f => f.Name.Equals(factionName, StringComparison.OrdinalIgnoreCase));
        if (faction == null)
            throw new InvalidOperationException("Faction not found.");

        JoinFaction(faction, character);
    }

    public void JoinFaction(Faction faction, Character character)
    {
        if (faction.Members.Contains(character))
            throw new InvalidOperationException("Character is already a member of this faction.");

        faction.Members.Add(character);
    }

    public void LeaveFaction(Faction faction, Character character)
    {
        if (faction.Members.Contains(character) == false)
            throw new InvalidOperationException("Character is not in a faction.");

        faction.Members.Remove(character);
    }



    public bool AreAllies(Character first, Character second)
    {
        return _factions.Any(f => f.Members.Contains(first) && f.Members.Contains(second));
    }

    public List<Faction> GetAllFactionsOfCharacter(Character character)
    {
        return _factions.Where(f => f.Members.Contains(character)).ToList();
    }
}
