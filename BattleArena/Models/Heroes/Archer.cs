namespace BattleArena.Models
{
    public class Archer : Hero
    {
        public Archer(string id = "A0")
        {
            base.MaxHealth = 100;
            base.Id = id;
            CurrentHealth = MaxHealth;
        }

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
