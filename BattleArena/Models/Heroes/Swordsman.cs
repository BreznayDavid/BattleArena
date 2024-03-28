namespace BattleArena.Models
{
    public class Swordsman : Hero
    {
        public Swordsman(string id = "S0")
        {
            base.MaxHealth = 120;
            base.Id = id;
            CurrentHealth = MaxHealth;
        }

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
