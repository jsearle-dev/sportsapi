using System.Data;
using SportsApi.Contracts;
using SportsApi.Models;

namespace SportsApi.Services
{
    public class TeamService : ITeamService
    {

        private readonly IDataService<Team> _dataService;

        public TeamService(IDataService<Team> dataService)
        {
            _dataService = dataService;
        }

        public async Task<Team> PersistTeamAsync(Team team)
        {
            if (string.IsNullOrWhiteSpace(team.TeamName) || string.IsNullOrWhiteSpace(team.Location))
            {
                throw new InvalidDataException();
            }

            var matchingTeams =  _dataService.Filter(t => t.TeamName == team.TeamName && t.Location == team.Location);

            if (matchingTeams.Any())
            {
                throw new DuplicateNameException();
            }

            return await _dataService.PersistAsync(team.Id, team);
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _dataService.GetAllAsync();
        }

        public async Task<Team> GetTeamByIdAsync(long id)
        {
            var team = await _dataService.FindByIdAsync(id);
            if (team == null)
            {
                throw new KeyNotFoundException($"id: {id} is not found");
            }
            return team;
        }

        public async Task<IEnumerable<Team>> GetTeamsOrderedByLocationAsync()
        {
            var teams = await _dataService.GetAllAsync();
            teams = teams ?? new List<Team>();

            return teams.OrderBy(t => t.Location);
        }

        public async Task<IEnumerable<Team>> GetTeamsOrderedByNameAsync()
        {
            var teams = await _dataService.GetAllAsync();
            teams = teams ?? new List<Team>();
            
            return teams.OrderBy(t => t.TeamName);
        }
    }
}