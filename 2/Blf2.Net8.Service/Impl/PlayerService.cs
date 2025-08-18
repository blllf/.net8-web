using Blf.Net8.Contracts;
using Blf2.Net8.Entitry;
using Blf2.Net8.Entitry.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blf2.Net8.Service.Impl {
    public class PlayerService : IPlayerService {
        private readonly IPlayerRepository _playerRepository;
        //private readonly IMapper _mapper;
        //ctor
        public PlayerService(IPlayerRepository playerRepository) {
            _playerRepository = playerRepository;
        }

        public List<PlayerDto> SearchPlayers(PlayerDto playerDto) {
            var query = _playerRepository.GetPlayersQuery();
            if (playerDto.Id != Guid.Empty && playerDto.Id != null) {
                query = query.Where(p => p.Id == playerDto.Id);
            }
            if (!string.IsNullOrWhiteSpace(playerDto.Account)) {
                query = query.Where(p => p.Account.Contains(playerDto.Account.Trim()));
            }
            var players = query.ToList();
            //var playerDtos = _mapper.Map<List<PlayerDto>>(players);
               
            return null;
        }
    }                                                                            
}
