namespace BattleArena.Models
{
    public class Knight : Hero
    {
        /// <summary>
        /// A lovas hős létrehozása
        /// </summary>
        /// <param name="id">Azonosító</param>
        public Knight(string id = "K0")
        {
            base.MaxHealth = 150;
            base.Id = id;
            CurrentHealth = MaxHealth;
        }

        /// <summary>
        /// Lovas támad
        /// • lovast: védekező meghal
        /// • kardost: lovas meghal
        /// • íjászt: íjász meghal
        /// </summary>
        /// <param name="opponent">Védekező hős</param>
        public override void Attack(Hero opponent)
        {
            if (opponent is Swordsman)
            {
                Killed();
                opponent.Survive();
                return;
            }
            
            opponent.Killed();
            Survive();
        }
    }
}
