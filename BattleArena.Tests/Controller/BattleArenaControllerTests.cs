using FakeItEasy;
using BattleArena.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Newtonsoft.Json;
using BattleArena.Models.Battle;
using BattleArena.Models;

namespace BattleArena.Tests.Controller
{
    public class BattleArenaControllerTests
    {

        private readonly ILogger<BattleArenaController> _logger;
        private readonly IWebHostEnvironment _environment;

        public BattleArenaControllerTests() {
            _logger = A.Fake<ILogger<BattleArenaController>>();
            _environment = A.Fake<IWebHostEnvironment>();
        }

        /// <summary>
        /// Ha kevesebb, mint 2 hőst rak be az arénába, akkor a csata nem indul el
        /// </summary>
        [Fact]
        public void BattleArenaController_Generate_ThrowException()
        {
            int n = 1;
            var controller = new BattleArenaController(_logger, _environment);

            var result = controller.Generate(n);

            result.Should().NotBeNull();
            result.Should().Be("None");
        }

        /// <summary>
        /// Ha legalább 2 hőst rak be az aréna, akkor folyhat a csata, visszatérésként érkezik egy véletlenszerű azonosító
        /// </summary>
        [Fact]
        public void BattleArenaController_Generate_Battle()
        {
            int n = 2;
            var controller = new BattleArenaController(_logger, _environment);

            var result = controller.Generate(n);

            result.Should().NotBeNull();
            result.Should().NotBe("None");
        }

        /// <summary>
        /// Arra az esetre, ha azon azonosítót választjuk ki, amelyhez nem tartozik csata
        /// </summary>
        [Fact]
        public void BattleArenaController_Battle_NotFound()
        {
            string id = "2b131a61-717f-4eb1-b6b8-73b71a773f8b";
            var controller = new BattleArenaController(_logger, _environment);

            var result = controller.Battle(id);

            result.Should().NotBeNull();
            result.Should().Be("Aréna nem található.");
        }

        /// <summary>
        /// Arra az esetre, ha azon azonosítót választjuk ki, amelyhez van csata
        /// </summary>
        [Fact]
        public void BattleArenaController_Battle_Found()
        {
            string id = "2b131a61-717f-4eb1-b6b8-73b71a773f8c";
            var controller = new BattleArenaController(_logger, _environment);

            var result = controller.Battle(id);

            result.Should().NotBeNull();
            result.Should().NotBe("Aréna nem található.");
        }

        /// <summary>
        /// Leellenőrzi a csata köreinek számát
        /// </summary>
        [Fact]
        public void BattleArenaController_Battle_RoundCount()
        {
            string id = "2b131a61-717f-4eb1-b6b8-73b71a773f8c";
            var controller = new BattleArenaController(_logger, _environment);
            var battle = controller.Battle(id);
            History battleHistory = JsonConvert.DeserializeObject<History>(battle);
            battleHistory.RoundCount.Should().Be(25);
            battleHistory.RoundCount.Should().Be(battleHistory.BattleRounds.Count);
        }

    }
}
