using Blf2.Net8.Entitry;
using Blf2.Net8.Entitry.DTOs;
using Blf2.Net8.Entitry.RequestFeature;
using Blf2.Net8.Entitry.ResponseType;

namespace Blf.Net8.Contracts {
    public interface IPlayerRepository : IRepositoryBase<Player> {
        Task<List<Player>> GetPlayers();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        Task<List<Player>> GetPlayersByCondition(PlayerParameter parameter);
        /// <summary>
        /// 分页查询Pro会有元数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PagedList<Player> GetPlayersByConditionPro(PlayerParameter parameter);
        Task<Player> GetPlayerById(Guid playerId);
        Task<Player> GetPlayerWithCharacters(Guid playerId);
        Task<(bool Success, object InsertCount)> InsertDataToDB(int number);
    }
}
