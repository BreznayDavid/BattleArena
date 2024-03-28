namespace BattleArena.Models
{
    public class Hero
    {

        /// <summary>
        /// A hős azonosítója, amellyel a csata eseményeinél megkülönböztethető
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// A hős aktuális életereje
        /// </summary>        
        public uint CurrentHealth { get; set; }

        /// <summary>
        /// A hős maximális életereje
        /// </summary>
        public uint MaxHealth { get; set; }

        /// <summary>
        /// A hős életereje a maximális negyedénél nagyobb-e. Ha igen, akkor harcra képes
        /// </summary>
        public bool IsAlive
        {
            get { 
                return CurrentHealth >= MaxHealth / 4; 
            }
        }

        /// <summary>
        /// A hős életerejének gyógyulása 10 egységgel vagy fel a maximumra
        /// </summary>
        public void Heal()
        {
            if (IsAlive)
            {
                CurrentHealth = Math.Min(CurrentHealth + 10, MaxHealth);
            }
        }

        /// <summary>
        /// A hős meghalt
        /// </summary>
        public void Killed()
        {
            CurrentHealth = 0;
        }

        /// <summary>
        /// A hős túlélte a csatát, így az életereje megfeleződött
        /// </summary>
        public void Survive()
        {
            CurrentHealth /= 2;
        }

        /// <summary>
        /// A hős támadása, melynek folyamatát a gyermekosztályai részletezi
        /// </summary>
        public virtual void Attack(Hero opponent)
        {
        }

    }
}
