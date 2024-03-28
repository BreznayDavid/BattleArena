namespace BattleArena.Models.Battle
{
    public class History
    {
        public int RoundCount { 
            get { return BattleRounds.Count; } 
        }
        public List<BattleRound> BattleRounds { get; set; } = new List<BattleRound>();

        public void AddBattleRound(BattleRound round)
        {
            BattleRounds.Add(round);
        }

    }
}
