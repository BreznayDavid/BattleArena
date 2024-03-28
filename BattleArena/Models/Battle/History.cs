namespace BattleArena.Models.Battle
{
    public class History
    {
     
        /// <summary>
        /// A csata köreinek száma
        /// </summary>
        public int RoundCount { 
            get { return BattleRounds.Count; } 
        }

        /// <summary>
        /// A csata köreinek adatait tartalmazó lista
        /// </summary>
        public List<BattleRound> BattleRounds { get; set; } = new List<BattleRound>();

        /// <summary>
        /// A csata legutóbbi körének felvétele
        /// </summary>
        /// <param name="round">Felvételre váró csatakör</param>
        public void AddBattleRound(BattleRound round)
        {
            BattleRounds.Add(round);
        }

    }
}
