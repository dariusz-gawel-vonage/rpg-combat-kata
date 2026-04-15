using RPGKataLogic.Enums;
using RPGKataLogic.Logic;
using RPGKataLogic.Models;

var mapHeight = 50;
var mapWidth = 50;
var random = new Random();
var factionService = new FactionService();
var mapService = new MapService(mapWidth, mapHeight);
var combatService = new CombatService(mapService, factionService);

var beings = new List<Being>();
for(int i = 0; i < 50; i++)
    CreateBeing(mapHeight, mapWidth, random, mapService, beings);

var round = 1;
while (true)
{
    await Task.Delay(250);
    Console.Clear();

    PlayRound(random, mapService, combatService, beings.Where(x => x is Fighter).ToList());
    PlayRound(random, mapService, combatService, beings.Where(x => x is Character && x is not Fighter).ToList());
    PlayRound(random, mapService, combatService, beings.Where(x => x is Item).ToList());

    Console.WriteLine($"{Environment.NewLine}{Environment.NewLine} ROUND {round} {Environment.NewLine}");
    mapService.DisplayMap();
    round++;
}

static void CreateBeing(int mapHeight, int mapWidth, Random random, MapService mapService, List<Being> beings)
{
    Being being = null;
    switch (random.Next(4))
    {
        case 0:
            var type = (FighterType)random.Next(2);
            being = new Fighter(1000, 1, type);
            break;
        case 1:
            being = new Character();
            break;
        case 2:
            being = new Tree(1000 + random.Next(1000));
            break;
        case 3:
            being = new Stone(10000 + random.Next(10000));
            break;
        default:
            throw new InvalidOperationException("Invalid being type");
    }
    beings.Add(being);
    var desiredGround = mapService.GetGround(random.Next(mapHeight), random.Next(mapWidth));
    mapService.SetBeingLocation(being, desiredGround);
}

static void PlayRound(Random random, MapService mapService, CombatService combatService, List<Being> beingsOnMap)
{
    foreach (var being in beingsOnMap)
    {
        try
        {
            if (being.Health <= 0)
                continue;

            if (being is Fighter fighter && random.Next(3) == 0)
            {
                var beingsInRange = mapService.GetBeingsInRange(fighter, fighter.GetAttackRange());
                if (beingsInRange.Count > 0)
                {
                    var target = beingsInRange[random.Next(beingsInRange.Count)];
                    combatService.Damage(fighter, target, random.Next(100));
                    continue;
                }
            }
            else if (being is Character character)
            {
                if (random.Next(10) == 0)
                {
                    combatService.Heal(character, character, random.Next(100));
                    continue;
                }
                else
                {
                    mapService.MoveBeingRandomWay(character);
                }
            }
        }
        finally
        {
            DisplayBeing(mapService, being);
        }
    }
}

static void DisplayBeing(MapService mapService, Being being)
{
    Console.Write($"{Environment.NewLine}");
    Console.Write(being.ToString());

    var location = mapService.GetBeingLocation(being);
    if (location == null)
        return;

    Console.Write($" LOCATION: {location.X}:{location.Y}");
}