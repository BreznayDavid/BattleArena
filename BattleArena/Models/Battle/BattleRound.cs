using Newtonsoft.Json;

namespace BattleArena.Models
{
    public class BattleRound
    {
        [JsonIgnore]
        public Hero Offender { get; set; }

        public string OffenderId { get; set; }

        public uint OffenderStartHealth { get; set; }

        public uint OffenderResultHealth { get; set; }

        [JsonIgnore]
        public Hero Defender { get; set; }

        public string DefenderId { get; set; }

        public uint DefenderStartHealth { get; set; }

        public uint DefenderResultHealth { get; set; }

        public void ChooseOpponents(List<Hero> heroes)
        {
            Offender = heroes[Random.Shared.Next(0, heroes.Count)];
            OffenderId = Offender.Id ?? "";
            OffenderStartHealth = Offender.CurrentHealth;

            Defender = heroes.Where(x => x != Offender).ToList()[Random.Shared.Next(0, heroes.Count - 2)];
            DefenderId = Defender.Id ?? "";
            DefenderStartHealth = Defender.CurrentHealth;
        }

        public void Fight()
        {
            Offender.Attack(Defender);
            OffenderResultHealth = Offender.CurrentHealth;
            DefenderResultHealth = Defender.CurrentHealth;
        }

    }
}
