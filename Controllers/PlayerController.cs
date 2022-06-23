#nullable disable
using Microsoft.AspNetCore.Mvc;
using SportsApi.Models;
using SportsApi.Contracts;
using SportsApi.Exceptions;
using SportsApi.Services;

namespace SportsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetPlayers()
        {
            var player = await _playerService.GetAllPlayersAsync();
            return Ok(player);
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(long id)
        {

            try
            {
                var player = await _playerService.GetPlayerByIdAsync(id);
                return Ok(player);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/Players/team/5
        [HttpGet("team/{teamId}")]
        public ActionResult<IEnumerable<Player>> GetPlayerByTeam(long teamId)
        {
            var players = _playerService.GetPlayersByTeam(teamId);
            return Ok(players);
        }

        // GET: api/Players/search/lastName/{lastName}
        [HttpGet("search/lastName/{lastName}")]
        public ActionResult<IEnumerable<Player>> GetPlayerByLastName(string lastName)
        {
            var player = _playerService.GetPlayersByLastName(lastName);
            return Ok(player);
        }

        // GET: api/Players/team/unaffiliated
        [HttpGet("team/unaffiliated")]
        public ActionResult<IEnumerable<Player>> GetUnaffiliatedPlayers()
        {
            var player = _playerService.GetUnaffiliatedPlayers();
            return Ok(player);
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(long id, Player player)
        {
            try
            {
                player = await _playerService.PersistPlayerAsync(player);
                return Ok(player);
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Players/5/team/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/team/{teamId}")]
        public async Task<IActionResult> PutPlayerTeam(long id, long teamId)
        {
            try
            {
                var player = await _playerService.SetTeamForPlayer(id, teamId);
                return Ok(player);
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Players/5/team
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpDelete("{id}/team")]
        public async Task<IActionResult> DeletePlayerTeam(long id)
        {
            var player = await _playerService.SetTeamForPlayer(id, null);
            return Ok(player);
        }

        // POST: api/Players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            try
            {
                player = await _playerService.PersistPlayerAsync(player);
                return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, player);
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}