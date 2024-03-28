using Microsoft.AspNetCore.Mvc;
using BattleArena.Models;
using Newtonsoft.Json;
using System.IO;

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
                Response.StatusCode = 400;
                _logger.LogError(e, e.Message);
                return "None";
            }
        }

        [HttpGet("Battle/{id}")]
        public string Battle(string id)
        {
            try
            {
                List<Arena> arenas = JsonConvert.DeserializeObject<List<Arena>>(System.IO.File.ReadAllText(Path.Combine(_environment.ContentRootPath, "Data", "battleArenaInfo.json"))) ?? new List<Arena>();
                if (!arenas.Exists(arena => arena.Id == id))
                {
                    Response.StatusCode = 404;
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
