using SportsApi.Models;

namespace SportsApi.Contracts
{
    public interface ITeamService 
    {

        // Create a team
        Task<Team> PersistTeamAsync(Team team);

        // Query by ID
        Task<Team> GetTeamByIdAsync(long id);

        // Query all teams
        Task<IEnumerable<Team>> GetAllTeamsAsync();

        // Query all teams by name or location
        Task<IEnumerable<Team>> GetTeamsOrderedByLocationAsync();

        Task<IEnumerable<Team>> GetTeamsOrderedByNameAsync();
    }
}
