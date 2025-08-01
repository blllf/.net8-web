using Blf.Net8.Contracts;
using Blf.Net8.EntityFramework;
using Blf.Net8.EntityFramework.Repositories;
using Blf2.Net8.Entitry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blf2.Net8.EntityFramework.Repositories {
    public class PlayerRepository : RepositoryBase<Player>, IPlayerRepository {

        // : base(...)调用了基类的构造函数，是连接子类和基类构造函数调用链的关键环节
        public PlayerRepository(GameManagermentDbContext dbContext) : base(dbContext) {
        }

        public async Task<Player> GetPlayerById(Guid playerId) {
            return await FindByCondition(player => player.Id == playerId).FirstOrDefaultAsync();
        }

        public async Task<List<Player>> GetPlayers() {
            return await GetAll().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<Player> GetPlayerWithCharacters(Guid playerId) {
            return await FindByCondition(player => player.Id == playerId).Include(player => player.Characters).FirstOrDefaultAsync();
        }

    
    }
}
