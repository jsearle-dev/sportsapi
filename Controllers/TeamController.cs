#nullable disable
using System.Data;
using Microsoft.AspNetCore.Mvc;
using SportsApi.Models;
using SportsApi.Contracts;
using SportsApi.Exceptions;
using SportsApi.Services;

namespace SportsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams);
        }

        // GET: api/Teams/ordered/location
        [HttpGet("ordered/location")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamsOrderedByLocation()
        {
            var teams = await _teamService.GetTeamsOrderedByLocationAsync();
            return Ok(teams);
        }

        // GET: api/Teams/ordered/name
        [HttpGet("ordered/name")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamsOrderedByName()
        {
            var teams = await _teamService.GetTeamsOrderedByNameAsync();
            return Ok(teams);
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(long id)
        {

            try
            {
                var team = await _teamService.GetTeamByIdAsync(id);
                return Ok(team);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // PUT: api/Teams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Team>> PutTeam(long id, Team team)
        {
            try 
            {
                team = await _teamService.PersistTeamAsync(team);
                return Ok(team);
            }
            catch (InvalidDataException)
            {
                return BadRequest();
            }
            catch (DuplicateNameException)
            {
                return Conflict();
            }
        }

        // POST: api/Teams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            try 
            {
                team = await _teamService.PersistTeamAsync(team);
                return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
            }
            catch (InvalidDataException)
            {
                return BadRequest();
            }
            catch (DuplicateNameException)
            {
                return Conflict();
            }
        }
    }
}
