using BattleArena.Models.Battle;
using System.IO;
using Newtonsoft.Json;

namespace BattleArena.Models
{
    public class Arena
    {
        [JsonProperty("Id")]
        public string Id { get; private set; }

        [JsonProperty("Heroes")]
        public List<Hero> Heroes { get; set; } = new List<Hero>();

        [JsonProperty("BattleHistory")]
        public History BattleHistory { get; set; } = new History();

        private static readonly string[] HeroTypes = new[]
        {
            "Archer", "Swordsman", "Knight"
        };

        public Arena(int members = 2)
        {
            Id = Guid.NewGuid().ToString();
            for(int i = 1; i <= members; i++)
            {
                int typeId = Random.Shared.Next(0, HeroTypes.Length);
                switch (HeroTypes[typeId])
                {
                    case "Archer":
                        Heroes.Add(new Archer("A"+i.ToString()));
                        break;
                    case "Swordsman":
                        Heroes.Add(new Swordsman("S" + i.ToString()));
                        break;
                    case "Knight":
                        Heroes.Add(new Knight("K" + i.ToString()));
                        break;
                }
            }
        }

        public void Battle()
        {
            List<Hero> aliveHeroes = GetAliveHeroes();
            BattleRound battleRound;
            while (aliveHeroes.Count > 1)
            {
                battleRound = new BattleRound();
                battleRound.ChooseOpponents(aliveHeroes);
                battleRound.Fight();
                BattleHistory.AddBattleRound(battleRound);

                foreach (Hero hero in GetRestingHeroes(battleRound))
                {
                    hero.Heal();
                }
                aliveHeroes = RejoinSurvivedHeroes(battleRound);
            }
        }

        private List<Hero> GetAliveHeroes()
        {
            return Heroes.Where(h => h.IsAlive).ToList();
        }

        private List<Hero> RejoinSurvivedHeroes(BattleRound battleRound)
        {
            List<Hero> fighters = new List<Hero> { battleRound.Offender, battleRound.Defender };
            return GetRestingHeroes(battleRound).Union(fighters.Where(x => x.IsAlive)).ToList();
        }

        private List<Hero> GetRestingHeroes(BattleRound battleRound)
        {
            return GetAliveHeroes().Where(x => x != battleRound.Offender && x != battleRound.Defender).ToList();
        }

        public string SaveBattleToJson(string path)
        {
            string storageFileName = Path.Combine(path, "Data", "battleArenaInfo.json");
            List<Arena> currentArenas = new List<Arena>();

            if (File.Exists(storageFileName))
            {
                currentArenas = JsonConvert.DeserializeObject<List<Arena>>(File.ReadAllText(storageFileName)) ?? new List<Arena>();
            }
            currentArenas.Add(this);
            File.WriteAllText(storageFileName, JsonConvert.SerializeObject(currentArenas, Formatting.Indented));
            return Id;
        }

    }
}
