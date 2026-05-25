## Prompt

### My prompt:
dariusz-gawel-vonage: Hey, my friend also did this Kata project. And I got a question from him. I modelled "Factions" as a list of strings on the character, you went for a list of characters on a faction object. I wonder if another couple of requirements would mark one solution as more maintainable than the other. 
Could you elaborate on that?

### Model:
Claude Opus 4.6

---

## The answer

Great question — this is a classic modeling trade-off. 

Let me compare both approaches:


### Friend's approach: Character with List<string> Factions

#### Where your friend's approach wins
- Simpler — no extra class, no `FactionService`, fewer moving parts
- Fast lookup: "What factions is this character in?" is O(1) — it's right on the character
- Sufficient if factions are just labels for an `AreAllies` check

### Your approach: Faction entity with List<Character> Members

#### Where your approach wins
- Factions as first-class entities — if a requirement comes in like "factions have a leader", "factions have a max size", or "factions can declare war on each other", you already have a place to put that behavior. Your friend would need to introduce a Faction class retroactively and migrate.
- Renaming a faction — you change one Faction.Name. Your friend must find and update every character carrying that string.
- Validation — your FactionService can enforce rules like "faction must exist before joining". With strings, there's nothing preventing `character.Factions.Add("typo")`.
- Querying members — "Who's in faction X?" is a simple faction.Members. Your friend has to scan all characters.

### The real differentiator: which direction do requirements grow?
| Future requirement | Strings on Character | Faction entity | 
|---|---|---| 
| Factions have properties (color, leader, alliance) | 🔴 Major refactor | 🟢 Add property | 
| Faction-to-faction relationships (war, alliance) | 🔴 No place to model | 🟢 Natural fit | 
| "List all members of X" | 🟠 Scan all characters | 🟢 Direct access | 
| "What factions is character in?" | 🟢 Direct access | 🟠 Scan all factions | 
| Keep it simple, factions are just tags | 🟢 Minimal code | 🟠 Over-engineered |

### Verdict
Your friend's approach is fine if factions are just labels — a grouping mechanism for the `AreAllies` check and nothing more. Your approach is better if factions are things in the domain that will grow behavior over time.

That said, your current implementation has a weakness the review flagged (issue #14): Faction.Members is a public List<Character>, so anyone can bypass `FactionService` and call `faction.Members.Add(...)` directly. If you exposed `IReadOnlyList<Character>` and routed mutations through methods on `Faction` itself, your approach would be strictly superior — because you'd get encapsulation and extensibility.

### TL;DR: 
Your friend optimized for simplicity today. You optimized for extensibility tomorrow. In a real codebase, factions almost always grow behavior, so your direction is the more maintainable one — as long as you tighten up the encapsulation.
