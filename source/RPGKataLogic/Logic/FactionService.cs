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

        faction.ClearMembers();
        _factions.Remove(faction);
    }

    public void UpdateFaction(string oldName, string newName)
    {
        var faction = _factions.FirstOrDefault(f => f.Name.Equals(oldName, StringComparison.OrdinalIgnoreCase));
        if (faction == null)
            throw new InvalidOperationException("Faction not found.");

        if (_factions.Any(f => f.Name.Equals(newName, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Faction with the same name already exists.");

        faction.Rename(newName);
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
        faction.AddMember(character);
    }

    public void LeaveFaction(Faction faction, Character character)
    {
        faction.RemoveMember(character);
    }

    public bool AreAllies(Character first, Character second)
    {
        return _factions.Any(f => f.HasMember(first) && f.HasMember(second));
    }

    public List<Faction> GetAllFactionsOfCharacter(Character character)
    {
        return _factions.Where(f => f.HasMember(character)).ToList();
    }
}
