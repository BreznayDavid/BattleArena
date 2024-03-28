namespace BattleArena.Models
{
    public class Archer : Hero
    {

        /// <summary>
        /// Az íjász hős létrehozása
        /// </summary>
        /// <param name="id">Azonosító</param>
        public Archer(string id = "A0")
        {
            base.MaxHealth = 100;
            base.Id = id;
            CurrentHealth = MaxHealth;
        }

        /// <summary>
        /// Az íjász támad
        /// • lovast: 40% eséllyel a lovas meghal, 60%-ban kivédi
        /// • kardost: kardos meghal
        /// • íjászt: védekező meghal
        /// </summary>
        /// <param name="opponent">Védekező hős</param>
        public override void Attack(Hero opponent)
        {
            if (opponent is Knight)
            {
                int battleResult = Random.Shared.Next(1, 10);
                if (battleResult <= 4)
                {
                    opponent.Killed();
                }
                else
                {
                    opponent.Survive();
                }
            }
            else
            {
                opponent.Killed();
            }
            Survive();
        }

    }
}
