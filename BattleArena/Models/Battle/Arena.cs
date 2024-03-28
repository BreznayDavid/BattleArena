using BattleArena.Models.Battle;
using Newtonsoft.Json;

namespace BattleArena.Models
{
 
    public class Arena
    {
        /// <summary>
        /// A csata azonosítója (UUID)
        /// </summary>
        [JsonProperty("Id")]
        public string Id { get; private set; }

        /// <summary>
        /// A csata hőseinek listája
        /// </summary>
        [JsonProperty("Heroes")]
        public List<Hero> Heroes { get; set; } = new List<Hero>();

        /// <summary>
        /// A csata eseményeit összefoglaló adatlista
        /// </summary>
        [JsonProperty("BattleHistory")]
        public History BattleHistory { get; set; } = new History();

        /// <summary>
        /// Hőstípusok listája
        /// </summary>
        private static readonly string[] HeroTypes = new[]
        {
            "Archer", "Swordsman", "Knight"
        };

        /// <summary>
        /// Az arénacsata létrehozása
        /// </summary>
        /// <param name="members">Hősök száma</param>
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

        /// <summary>
        /// Csata elindítása addig, ameddig maximum 1 hős áll talpon.
        /// </summary>
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

        /// <summary>
        /// Csata indítása a paraméterekben kiválasztott hősökkel
        /// </summary>
        /// <param name="offender">Támadó hős</param>
        /// <param name="defender">Védekező hős</param>
        public void Battle(Hero offender, Hero defender)
        {
            BattleRound battleRound;
            battleRound = new BattleRound();
            battleRound.ChooseOpponents(offender, defender);
            battleRound.Fight();
            BattleHistory.AddBattleRound(battleRound);
            foreach (Hero hero in GetRestingHeroes(battleRound))
            {
                hero.Heal();
            }
        }

        /// <summary>
        /// Kilistázza a túlélő hősöket
        /// </summary>
        /// <returns>Túlélő hősök listája</returns>
        private List<Hero> GetAliveHeroes()
        {
            return Heroes.Where(h => h.IsAlive).ToList();
        }

        /// <summary>
        /// Begyűjti a kör túlélőit a pihenő hősök közé
        /// </summary>
        /// <param name="battleRound">A csata aktuális köre</param>
        /// <returns>A csatában megmaradt hősök listája</returns>
        private List<Hero> RejoinSurvivedHeroes(BattleRound battleRound)
        {
            List<Hero> fighters = new List<Hero> { battleRound.Offender, battleRound.Defender };
            return GetRestingHeroes(battleRound).Union(fighters.Where(x => x.IsAlive)).ToList();
        }

        /// <summary>
        /// Azon hősök begyűjtése, akik kimaradnak az aktuális körből
        /// </summary>
        /// <param name="battleRound">A csata aktuális köre</param>
        /// <returns>A körből kimaradó hősök listája</returns>
        private List<Hero> GetRestingHeroes(BattleRound battleRound)
        {
            return GetAliveHeroes().Where(x => x != battleRound.Offender && x != battleRound.Defender).ToList();
        }

        /// <summary>
        /// A csata adatainak eltárolása JSON-ba
        /// </summary>
        /// <param name="path">A projekt főmappájának elérési útvonala</param>
        /// <returns>Az eltárolt csata azonosítója (UUID)</returns>
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
