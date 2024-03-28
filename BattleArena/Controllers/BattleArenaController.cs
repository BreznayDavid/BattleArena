using Microsoft.AspNetCore.Mvc;
using BattleArena.Models;
using Newtonsoft.Json;

namespace BattleArena.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleArenaController : ControllerBase
    {

        private readonly ILogger<BattleArenaController> _logger;
        private readonly IWebHostEnvironment _environment;

        public BattleArenaController(ILogger<BattleArenaController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        /// <summary>
        /// Létrehoz egy arénacsatát a megadott számú, véletlenszerűen generált hősök számával
        /// </summary>
        /// <param name="n">Hősök száma</param>
        /// <returns>A kigenerált csata azonosítója</returns>
        [HttpPost("Generate/{n}")]
        public string Generate(int n)
        {
            try
            {
                if (n < 2)
                {
                    throw new Exception("Legalább 2 hős szerepelhet egy csatában!");
                }

                Arena battleArena = new Arena(n);
                battleArena.Battle();
                return battleArena.SaveBattleToJson(_environment.ContentRootPath);
            }
            catch(Exception e)
            {
                if (Response != null)
                {
                    Response.StatusCode = 400;
                }
                _logger.LogError(e, e.Message);
                return "None";
            }
        }

        /// <summary>
        /// Lekérdezi a megadott azonosítójú csata folyamatát és annak köreinek számát.
        /// </summary>
        /// <param name="id">A csata azonosítója (UUID)</param>
        /// <returns>History osztály a köreinek számával és a csata folyamatával</returns>
        [HttpGet("Battle/{id}")]
        public string Battle(string id)
        {
            try
            {
                List<Arena> arenas = JsonConvert.DeserializeObject<List<Arena>>(System.IO.File.ReadAllText(Path.Combine(_environment.ContentRootPath, "Data", "battleArenaInfo.json"))) ?? new List<Arena>();
                if (!arenas.Exists(arena => arena.Id == id))
                {
                    if (Response != null)
                    {
                        Response.StatusCode = 404;
                    }
                    throw new Exception("Aréna nem található.");
                }
                return JsonConvert.SerializeObject(arenas.Find(arena => arena.Id == id)?.BattleHistory);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return e.Message;
            }
        }
    }
}
