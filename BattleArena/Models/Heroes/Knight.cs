namespace BattleArena.Models
{
    public class Knight : Hero
    {

        public Knight(string id = "K0")
        {
            base.MaxHealth = 150;
            base.Id = id;
            CurrentHealth = MaxHealth;
        }

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
