using FakeItEasy;
using FluentAssertions;
using BattleArena.Models;

namespace BattleArena.Tests
{
    public partial class BattleTests
    {

        /// <summary>
        /// Hármas párbaj
        /// </summary>
        [Fact]
        public void Battle_Threeway()
        {
            Arena arena = new Arena(0);
            arena.Heroes.Add(new Swordsman("S1"));
            arena.Heroes.Add(new Knight("K2"));
            arena.Heroes.Add(new Archer("A3"));

            BattleRound battleRound = new BattleRound();
            battleRound.ChooseOpponents(arena.Heroes[0], arena.Heroes[2]);
            battleRound.Fight();

            arena.Heroes[0].IsAlive.Should().BeTrue();
            arena.Heroes[0].CurrentHealth.Should().Be(60);
            arena.Heroes[1].IsAlive.Should().BeTrue();
            arena.Heroes[1].CurrentHealth.Should().Be(150);
            arena.Heroes[2].IsAlive.Should().BeFalse();
            arena.Heroes[2].CurrentHealth.Should().Be(0);
            arena.Heroes.Where(x => x.IsAlive).ToList().Count.Should().Be(2);

            battleRound = new BattleRound();
            battleRound.ChooseOpponents(arena.Heroes[1], arena.Heroes[0]);
            battleRound.Fight();

            arena.Heroes[0].IsAlive.Should().BeTrue();
            arena.Heroes[0].CurrentHealth.Should().Be(30);
            arena.Heroes[1].IsAlive.Should().BeFalse();
            arena.Heroes[1].CurrentHealth.Should().Be(0);
            arena.Heroes.Where(x => x.IsAlive).ToList().Count.Should().Be(1);
        }

        /// <summary>
        /// Két csata is tovább él a hős, vagyis a teljes életerejének 25%-ánál is él
        /// </summary>
        [Fact]
        public void Battle_FullHero()
        {
            Arena arena = new Arena(0);
            arena.Heroes.Add(new Knight("K1"));
            arena.Heroes.Add(new Knight("K2"));
            arena.Heroes.Add(new Archer("A3"));

            BattleRound battleRound = new BattleRound();
            battleRound.ChooseOpponents(arena.Heroes[0], arena.Heroes[1]);
            battleRound.Fight();

            battleRound = new BattleRound();
            battleRound.ChooseOpponents(arena.Heroes[0], arena.Heroes[2]);
            battleRound.Fight();

            arena.Heroes[0].IsAlive.Should().BeTrue();
            arena.Heroes[0].CurrentHealth.Should().BeGreaterThanOrEqualTo(arena.Heroes[0].MaxHealth / 4);
        }

        /// <summary>
        /// Ha az életereje a maximumnak a 25%-a alá kerül, meghal
        /// </summary>
        [Fact]
        public void Battle_HeroicDeath()
        {
            Arena arena = new Arena(0);
            arena.Heroes.Add(new Knight("K1"));
            arena.Heroes.Add(new Knight("K2"));
            arena.Heroes.Add(new Archer("A3"));
            arena.Heroes.Add(new Archer("A4"));


            BattleRound battleRound;
            for(int i = 1; i <= 3; i++)
            {
                battleRound = new BattleRound();
                battleRound.ChooseOpponents(arena.Heroes[0], arena.Heroes[i]);
                battleRound.Fight();
            }

            arena.Heroes[0].IsAlive.Should().BeFalse();
            arena.Heroes[0].CurrentHealth.Should().BeLessThan(arena.Heroes[0].MaxHealth / 4);
        }

        /// <summary>
        /// A pihenő hősök gyógyulása
        /// </summary>
        [Fact]
        public void Battle_RestOurHeroes()
        {
            Arena arena = new Arena(0);
            arena.Heroes.Add(new Knight("K1"));
            arena.Heroes.Add(new Knight("K2"));
            arena.Heroes.Add(new Archer("A3"));
            arena.Heroes.Add(new Archer("A4"));

            Hero offender, defender;
            BattleRound battleRound = new BattleRound();
            offender = arena.Heroes[0];
            defender = arena.Heroes[2];
            battleRound.ChooseOpponents(offender, defender);
            battleRound.Fight();

            string[] battledHeroes = new string[2] { offender.Id, defender.Id };
            foreach (Hero hero in arena.Heroes.Where(x => x.IsAlive && !battledHeroes.Contains(x.Id)).ToList())
            {
                hero.Heal();
            }
            arena.Heroes[0].IsAlive.Should().BeTrue();
            arena.Heroes[0].CurrentHealth.Should().Be(75);
            arena.Heroes[1].IsAlive.Should().BeTrue();
            arena.Heroes[1].CurrentHealth.Should().Be(150);
            arena.Heroes[2].IsAlive.Should().BeFalse();
            arena.Heroes[2].CurrentHealth.Should().Be(0);
            arena.Heroes[3].IsAlive.Should().BeTrue();
            arena.Heroes[3].CurrentHealth.Should().Be(100);

            offender = arena.Heroes[1];
            defender = arena.Heroes[3];
            battleRound.ChooseOpponents(offender, defender);
            battleRound.Fight();

           battledHeroes = new string[2] { offender.Id, defender.Id };
            foreach (Hero hero in arena.Heroes.Where(x => x.IsAlive && !battledHeroes.Contains(x.Id)).ToList())
            {
                hero.Heal();
            }
            arena.Heroes[0].IsAlive.Should().BeTrue();
            arena.Heroes[0].CurrentHealth.Should().Be(85);
            arena.Heroes[1].IsAlive.Should().BeTrue();
            arena.Heroes[1].CurrentHealth.Should().Be(75);
            arena.Heroes[2].IsAlive.Should().BeFalse();
            arena.Heroes[2].CurrentHealth.Should().Be(0);
            arena.Heroes[3].IsAlive.Should().BeFalse();
            arena.Heroes[3].CurrentHealth.Should().Be(0);
        }

        /// <summary>
        /// Az adott hős teljes gyógyulása
        /// </summary>
        [Fact]
        public void Battle_HealToTheMax()
        {
            Arena arena = new Arena(0);
            for(int i = 1; i <= 20; i++)
            {
                arena.Heroes.Add(new Knight("K"+i.ToString()));
            }
            int last = 19;

            int first = 0;

            Hero offender, defender;
            BattleRound battleRound;
            string[] battledHeroes;

            battleRound = new BattleRound();
            offender = arena.Heroes[first];
            defender = arena.Heroes[last - first];
            battleRound.ChooseOpponents(offender, defender);
            battleRound.Fight();
            battledHeroes = new string[2] { offender.Id, defender.Id };
            foreach (Hero hero in arena.Heroes.Where(x => x.IsAlive && !battledHeroes.Contains(x.Id)).ToList())
            {
                hero.Heal();
            }

            for(int j = 1; j <= 9; j++)
            {
                battleRound = new BattleRound();
                offender = arena.Heroes[j];
                defender = arena.Heroes[last - j];
                battleRound.ChooseOpponents(offender, defender);
                battleRound.Fight();
                battledHeroes = new string[2] { offender.Id, defender.Id };
                foreach (Hero hero in arena.Heroes.Where(x => x.IsAlive && !battledHeroes.Contains(x.Id)).ToList())
                {
                    hero.Heal();
                }

                switch (j)
                {
                    case 7:
                        arena.Heroes[0].CurrentHealth.Should().Be(145);
                        break;
                    case 8:
                    case 9:
                        arena.Heroes[0].CurrentHealth.Should().Be(150);
                        break;
                }
            }


        }

    }
}
