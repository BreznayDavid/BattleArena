namespace BattleArena.Models
{
    public class Hero
    {

        public string? Id { get; set; }
        
        public uint CurrentHealth { get; set; }

        public uint MaxHealth { get; set; }

        public bool IsAlive
        {
            get { 
                return CurrentHealth >= MaxHealth / 4; 
            }
        }

        public void Heal()
        {
            if (IsAlive)
            {
                CurrentHealth = Math.Min(CurrentHealth + 10, MaxHealth);
            }
        }

        public void Killed()
        {
            CurrentHealth = 0;
        }

        public void Survive()
        {
            CurrentHealth /= 2;
        }

        public virtual void Attack(Hero opponent)
        {
        }

    }
}
