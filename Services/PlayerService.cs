using SportsApi.Contracts;
using SportsApi.Models;

namespace SportsApi.Services
{
    public class PlayerService : IPlayerService
    {

        private readonly IDataService<Player> _dataService;

        public PlayerService(IDataService<Player> dataService)
        {
            _dataService = dataService;
        }


        public async Task<Player> PersistPlayerAsync(Player player)
        {
            if(player.TeamId != null) 
            {
                var players = _dataService.Filter(p => player.TeamId == p.TeamId && player.Id != p.Id);
                if(players.Count() >= 8)
                {
                    throw new InvalidDataException("Teams can only have 8 players.");
                }
            }
            return await _dataService.PersistAsync(player.Id, player);
        }

        public async Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            return await _dataService.GetAllAsync();
        }

        public IEnumerable<Player> GetPlayersByLastName(string lastName)
        {
            var teams = _dataService.Filter(p => p.LastName?.ToLower() == lastName.ToLower());
            teams = teams ?? new List<Player>();
            
            return teams;
        }

        public async Task<Player> GetPlayerByIdAsync(long id)
        {
            var player = await _dataService.FindByIdAsync(id);
            if (player == null)
            {
                throw new KeyNotFoundException($"id: {id} is not found");
            }
            return player;
        }

        public IEnumerable<Player> GetPlayersByTeam(long teamId)
        {
            var player = _dataService.Filter(p => p.TeamId != null && p.TeamId == teamId);
            player = player ?? new List<Player>();

            return player;
        }

        public IEnumerable<Player> GetUnaffiliatedPlayers()
        {
            var player = _dataService.Filter(p => p.TeamId == null);
            player = player ?? new List<Player>();

            return player;
        }

        public async Task<Player> SetTeamForPlayer(long playerId, long? teamId)
        {
            var player = await GetPlayerByIdAsync(playerId);
            player.TeamId = teamId;

            player = await PersistPlayerAsync(player);

            return player;
        }
    }
}