using Blf2.Net8.Entitry;
using Blf2.Net8.Entitry.DTOs;

namespace Blf.Net8.Contracts {
    public interface IPlayerRepository : IRepositoryBase<Player> {
        Task<List<Player>> GetPlayers();
        Task<Player> GetPlayerById(Guid playerId);
        Task<Player> GetPlayerWithCharacters(Guid playerId);

        
    }
}
