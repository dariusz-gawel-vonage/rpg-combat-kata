namespace RPGKataLogic.Models;

public class Faction
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Character> Members { get; set; }

    public Faction(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        Members = new List<Character>();
    }
}
