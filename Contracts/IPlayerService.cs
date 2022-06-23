using SportsApi.Models;

namespace SportsApi.Contracts
{
    public interface IPlayerService 
    {

        // Persist a player
        Task<Player> PersistPlayerAsync(Player player);

        //Set team for player
        Task<Player> SetTeamForPlayer(long playerId, long? teamId);

        // Query by ID
        Task<Player> GetPlayerByIdAsync(long id);

        // Query all players
        Task<IEnumerable<Player>> GetAllPlayersAsync();

        // Query unaffiliated players
        IEnumerable<Player> GetUnaffiliatedPlayers();

        // Query all players by name or team
        IEnumerable<Player> GetPlayersByTeam(long teamId);

        IEnumerable<Player> GetPlayersByLastName(string LastName);
    }
}
