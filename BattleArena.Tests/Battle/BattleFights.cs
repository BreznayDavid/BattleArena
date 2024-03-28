using FakeItEasy;
using FluentAssertions;
using BattleArena.Models;

namespace BattleArena.Tests
{
    public partial class BattleTests
    {

        /// <summary>
        /// Íjász vs íjász --> védekező meghal
        /// </summary>
        [Fact]
        public void Battle_Fight_ArcherVsArcher()
        {
            Arena arena = new Arena(0);
            arena.Heroes.Add(new Archer("A1"));
            arena.Heroes.Add(new Archer("A2"));

            BattleRound battleRound = new BattleRound();
            battleRound.ChooseOpponents(arena.Heroes[0], arena.Heroes[1]);
            battleRound.Fight();

            arena.Heroes[0].IsAlive.Should().BeTrue();
            arena.Heroes[0].CurrentHealth.Should().Be(50);
            arena.Heroes[1].IsAlive.Should().BeFalse();
            arena.Heroes[1].CurrentHealth.Should().Be(0);
        }

        /// <summary>
        /// Íjász vs kardos --> kardos meghal
        /// </summary>
        [Fact]
        public void BattleArenaController_Battle_ArcherVsSwordsman()
        {
            Arena arena = new Arena(0);
            arena.Heroes.Add(new Archer("A1"));
            arena.Heroes.Add(new Swordsman("S2"));

            BattleRound battleRound = new BattleRound();
            battleRound.ChooseOpponents(arena.Heroes[0], arena.Heroes[1]);
            battleRound.Fight();

            arena.Heroes[0].IsAlive.Should().BeTrue();
            arena.Heroes[0].CurrentHealth.Should().Be(50);
            arena.Heroes[1].IsAlive.Should().BeFalse();
            arena.Heroes[1].CurrentHealth.Should().Be(0);
        }

        /// <summary>
        /// Íjász vs lovas --> 40% eséllyel a lovas meghal, 60%-ban kivédi
        /// </summary>
        [Fact]
        public void BattleArenaController_Battle_ArcherVsKnight()
        {
            Arena arena = new Arena(0);
            arena.Heroes.Add(new Archer("A1"));
            arena.Heroes.Add(new Knight("K2"));

            BattleRound battleRound = new BattleRound();
            battleRound.ChooseOpponents(arena.Heroes[0], arena.Heroes[1]);
            battleRound.Fight();

            arena.Heroes[0].IsAlive.Should().BeTrue();
            arena.Heroes[0].CurrentHealth.Should().Be(50);
            
            if (arena.Heroes.Where(x => x.IsAlive).ToList().Count == 2)
            {
                // Ha a lovas kivédi
                arena.Heroes[1].IsAlive.Should().BeTrue();
                arena.Heroes[1].CurrentHealth.Should().Be(75);
            }
            else
            {
                // Ha a lovas meghal
                arena.Heroes[1].IsAlive.Should().BeFalse();
                arena.Heroes[1].CurrentHealth.Should().Be(0);
            }
        }

        /// <summary>
        /// Kardos vs íjász --> íjász meghal
        /// </summary>
        [Fact]
        public void BattleArenaController_Battle_SwordsmanVsArcher()
        {
            Arena arena = new Arena(0);
            arena.Heroes.Add(new Swordsman("S1"));
            arena.Heroes.Add(new Archer("A2"));

            BattleRound battleRound = new BattleRound();
            battleRound.ChooseOpponents(arena.Heroes[0], arena.Heroes[1]);
            battleRound.Fight();

            arena.Heroes[0].IsAlive.Should().BeTrue();
            arena.Heroes[0].CurrentHealth.Should().Be(60);
            arena.Heroes[1].IsAlive.Should().BeFalse();
            arena.Heroes[1].CurrentHealth.Should().Be(0);
        }

        /// <summary>
        /// Kardos vs kardos --> védekező meghal
        /// </summary>
        [Fact]
        public void BattleArenaController_Battle_SwordsmanVsSwordsman()
        {
            Arena arena = new Arena(0);
            arena.Heroes.Add(new Swordsman("S1"));
            arena.Heroes.Add(new Swordsman("S2"));

            BattleRound battleRound = new BattleRound();
            battleRound.ChooseOpponents(arena.Heroes[0], arena.Heroes[1]);
            battleRound.Fight();

            arena.Heroes[0].IsAlive.Should().BeTrue();
            arena.Heroes[0].CurrentHealth.Should().Be(60);
            arena.Heroes[1].IsAlive.Should().BeFalse();
            arena.Heroes[1].CurrentHealth.Should().Be(0);
        }

        /// <summary>
        /// Kardos vs lovas --> nem történik semmi
        /// </summary>
        [Fact]
        public void BattleArenaController_Battle_SwordsmanVSKnight()
        {
            Arena arena = new Arena(0);
            arena.Heroes.Add(new Swordsman("S1"));
            arena.Heroes.Add(new Knight("K2"));

            BattleRound battleRound = new BattleRound();
            battleRound.ChooseOpponents(arena.Heroes[0], arena.Heroes[1]);
            battleRound.Fight();

            arena.Heroes[0].IsAlive.Should().BeTrue();
            arena.Heroes[0].CurrentHealth.Should().Be(120);
            arena.Heroes[1].IsAlive.Should().BeTrue();
            arena.Heroes[1].CurrentHealth.Should().Be(150);
        }


        /// <summary>
        /// Lovas vs íjász --> íjász meghal
        /// </summary>
        [Fact]
        public void BattleArenaController_Battle_KnightVsArcher()
        {
            Arena arena = new Arena(0);
            arena.Heroes.Add(new Knight("K1"));
            arena.Heroes.Add(new Archer("A2"));

            BattleRound battleRound = new BattleRound();
            battleRound.ChooseOpponents(arena.Heroes[0], arena.Heroes[1]);
            battleRound.Fight();

            arena.Heroes[0].IsAlive.Should().BeTrue();
            arena.Heroes[0].CurrentHealth.Should().Be(75);
            arena.Heroes[1].IsAlive.Should().BeFalse();
            arena.Heroes[1].CurrentHealth.Should().Be(0);
        }

        /// <summary>
        /// Lovas vs kardos --> lovas meghal
        /// </summary>
        [Fact]
        public void BattleArenaController_Battle_KnightVsSwordsman()
        {
            Arena arena = new Arena(0);
            arena.Heroes.Add(new Knight("K1"));
            arena.Heroes.Add(new Swordsman("S2"));

            BattleRound battleRound = new BattleRound();
            battleRound.ChooseOpponents(arena.Heroes[0], arena.Heroes[1]);
            battleRound.Fight();

            arena.Heroes[0].IsAlive.Should().BeFalse();
            arena.Heroes[0].CurrentHealth.Should().Be(0);
            arena.Heroes[1].IsAlive.Should().BeTrue();
            arena.Heroes[1].CurrentHealth.Should().Be(60);
        }

        /// <summary>
        /// Lovas vs lovas --> védekező meghal
        /// </summary>
        [Fact]
        public void BattleArenaController_Battle_KnightVSKnight()
        {
            Arena arena = new Arena(0);
            arena.Heroes.Add(new Knight("K1"));
            arena.Heroes.Add(new Knight("K2"));

            BattleRound battleRound = new BattleRound();
            battleRound.ChooseOpponents(arena.Heroes[0], arena.Heroes[1]);
            battleRound.Fight();

            arena.Heroes[0].IsAlive.Should().BeTrue();
            arena.Heroes[0].CurrentHealth.Should().Be(75);
            arena.Heroes[1].IsAlive.Should().BeFalse();
            arena.Heroes[1].CurrentHealth.Should().Be(0);
        }

    }
}
