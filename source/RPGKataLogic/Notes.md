## Assumptions:
- Map is rectangle.
- 1 unit in the map is 1 meter.
- Healer could be only a `character`, not the item (but in theory I can imagine a magic tree that is healing nearby characters etc.).
- They can heal themself only up to the 1000 health points.

## Problems:
- `CalculateFinalDamage` is coupled to `Characters` and `Items` in a single place, because the instructions weren't clear wether the being can have a level like a character.
	If yes, then move `Level` property into a `Being`, if not. Then move this calculation somewhere else, but free coupling!
	- One more coupling in this class is `if (target is Character character && _factionService.AreAllies(attacker, character))`.
- `CalculateDistance` understand `Ground?` but does not provide `HasValue` method. 
	Replace `if (attackerDistance == null || targetDistance == null)` with `if (attackerDistance.HasValue == false || targetDistance.HasValue == false)`.

## To be done:

### Logic
1. Verify `CalculateDistance` calculations.
1. Make `MapService` able to set location, populate things etc.
1. (?) Logic for not having 2 beings at the same ground.
1. (?) Maybe only characters should be able to move.

### Coding
1. Implement more interfaces.
1. Try to use Structs and Records where possible.
1. Verify access modifiers and correct if needed.
1. Add tests.
1. (?) Add `Id` to `Character`.
1. (?) Remove not necessary functions i.e. in from `FactionService`.
