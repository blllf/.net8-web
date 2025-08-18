using Blf2.Net8.Entitry;
using Blf2.Net8.Entitry.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blf2.Net8.Service {
    public interface IPlayerService {
        //搜索
        //获取玩家信息根据Id、Account
        public List<PlayerDto> SearchPlayers(PlayerDto playerDto);

        //筛选
        //排序
    }
}
