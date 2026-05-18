namespace RPGKataLogic.Models;

public class Faction
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    private List<Character> _members;

    public IReadOnlyList<Character> Members => _members.AsReadOnly();

    public Faction(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        _members = new List<Character>();
    }

    public void AddMember(Character character)
    {
        if (HasMember(character))
            throw new InvalidOperationException("Character is already a member of this faction.");

        _members.Add(character);
    }

    public void RemoveMember(Character character)
    {
        if (HasMember(character) == false)
            throw new InvalidOperationException("Character is not in a faction.");

        _members.Remove(character);
    }

    public void ClearMembers()
    {
        _members.Clear();
    }

    public bool HasMember(Character character)
    {
        return _members.Contains(character);
    }
}
