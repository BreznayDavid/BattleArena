namespace BattleArena.Models
{
    public class Swordsman : Hero
    {
        /// <summary>
        /// A kardos hős létrehozása
        /// </summary>
        /// <param name="id">Azonosító</param>
        public Swordsman(string id = "S0")
        {
            base.MaxHealth = 120;
            base.Id = id;
            CurrentHealth = MaxHealth;
        }

        /// <summary>
        /// Kardos támad
        /// • lovast: nem történik semmi
        /// • kardost: védekező meghal
        /// • íjászt: íjász meghal
        /// </summary>
        /// <param name="opponent">Védekező hős</param>
        public override void Attack(Hero opponent)
        {
            if (opponent is Knight)
            {
                return;
            }
            opponent.Killed();
            Survive();
        }
    }
}
