using Newtonsoft.Json;

namespace BattleArena.Models
{
    public class BattleRound
    {
        /// <summary>
        /// A kör támadó hőse
        /// </summary>
        [JsonIgnore]
        public Hero Offender { get; set; }

        /// <summary>
        /// A kör támadó hősének azonosítója
        /// </summary>
        public string OffenderId { get; set; }

        /// <summary>
        /// A támadó kezdő életereje
        /// </summary>
        public uint OffenderStartHealth { get; set; }

        /// <summary>
        /// A támadó csata utáni életereje
        /// </summary>
        public uint OffenderResultHealth { get; set; }

        /// <summary>
        /// A védekező hős
        /// </summary>
        [JsonIgnore]
        public Hero Defender { get; set; }

        /// <summary>
        /// A kör támadó hősének azonosítója
        /// </summary>
        public string DefenderId { get; set; }

        /// <summary>
        /// A védekező kezdő életereje
        /// </summary>
        public uint DefenderStartHealth { get; set; }

        /// <summary>
        /// A védekező csata utáni életereje
        /// </summary>
        public uint DefenderResultHealth { get; set; }

        /// <summary>
        /// A csatakör hőseinek véletlenszerű kiválasztása
        /// </summary>
        /// <param name="heroes">Választható hősök</param>
        public void ChooseOpponents(List<Hero> heroes)
        {
            Hero offender = heroes[Random.Shared.Next(0, heroes.Count)];
            Hero defender = heroes.Where(x => x != offender).ToList()[Random.Shared.Next(0, heroes.Count - 2)];
            ChooseOpponents(offender, defender);
        }

        /// <summary>
        /// A csatakör hőseinek kiválasztása
        /// </summary>
        /// <param name="heroes">Választható hősök</param>
        public void ChooseOpponents(Hero offender, Hero defender)
        {
            Offender = offender;
            OffenderId = offender.Id;
            OffenderStartHealth = Offender.CurrentHealth;

            Defender = defender;
            DefenderId = defender.Id;
            DefenderStartHealth = Defender.CurrentHealth;
        }

        /// <summary>
        /// Támadás folyamata
        /// </summary>
        public void Fight()
        {
            Offender.Attack(Defender);
            OffenderResultHealth = Offender.CurrentHealth;
            DefenderResultHealth = Defender.CurrentHealth;
        }

    }
}
