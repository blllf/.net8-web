using Blf.Net8.Contracts;
using Blf.Net8.EntityFramework;
using Blf.Net8.EntityFramework.Repositories;
using Blf2.Net8.Entitry;
using Blf2.Net8.Entitry.RequestFeature;
using Blf2.Net8.Entitry.ResponseType;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
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

        // 分页查询
        public async Task<List<Player>> GetPlayersByCondition(PlayerParameter parameter) {
            return await GetAll()
                   .OrderBy(x => x.DateCreate)
                   .Skip((parameter.pageNumber - 1) * parameter.pageSize)
                   .Take(parameter.pageSize)
                   .ToListAsync();
        }

        public PagedList<Player> GetPlayersByConditionPro(PlayerParameter parameter) {
            return  GetAll()
                   .OrderBy(x => x.DateCreate)
                   .ToPagedList(parameter.pageNumber, parameter.pageSize);
        }

        public async Task<Player> GetPlayerWithCharacters(Guid playerId) {
            return await FindByCondition(player => player.Id == playerId).Include(player => player.Characters).FirstOrDefaultAsync();
        }

        public async Task<(bool Success, object InsertCount)> InsertDataToDB(int number) {
            if (number <= 0) { 
                return (false , 0);
            }
            try {
                var userAccounts = await _dbContext.Players
                    .Where(p => p.Account.StartsWith("user"))
                    .Select(p => p.Account) // 只取 Account 字符串
                    .ToListAsync(); // 执行查询，结果到内存

                int maxAccountId = 0;

                if (userAccounts.Any()) {
                    var accountIds = userAccounts
                        .Select(account => {
                            // 移除 "user" 前缀
                            if (account.Length > 4) {
                                string numStr = account.Substring(4);
                                if (int.TryParse(numStr, out int id)) {
                                    return (Success: true, Id: id);
                                }
                            }
                            // 如果格式不对，返回 (false, 0)
                            return (Success: false, Id: 0);
                        })
                        .Where(result => result.Success) // 只保留解析成功的
                        .Select(result => result.Id);    // 取出 Id

                    // 找出最大值
                    maxAccountId = accountIds.DefaultIfEmpty(0).Max();
                }
                int startId = maxAccountId + 1; // 下一个可用的 ID

                // 生成从 0 开始的 number 个整数 : 0 , 1 , 2 ... number - 1
                var players = Enumerable.Range(startId, number).Select(i => new Player() {
                    //Id = Guid.NewGuid(),
                    Account = "user" + i,
                    AccountType = "User"
                }).ToList();
                
                await _dbContext.AddRangeAsync(players);
                await _dbContext.SaveChangesAsync();
                return (true , players.Count);
            }
            catch (Exception e) {
                return (false , e);
            }
   
        }
    }
}
