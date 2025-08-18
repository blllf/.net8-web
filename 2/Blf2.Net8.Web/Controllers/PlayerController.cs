using AutoMapper;
using Blf.Net8.Contracts;
using Blf.Net8.EntityFramework;
using Blf2.Net8.Entitry;
using Blf2.Net8.Entitry.DTOs;
using Blf2.Net8.Entitry.RequestFeature;
using Blf2.Net8.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Blf2.Net8.Web.Controllers {


    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase {
        private readonly IPlayerService _playerService;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
      
        // 构造函数
        public PlayerController(IPlayerRepository playerRepository, IMapper mapper, IPlayerService playerService) {
            _playerRepository = playerRepository;
            _mapper = mapper;
            _playerService = playerService;
        }

        /// <summary>
        /// 获取所有玩家(手动映射)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllPlayers")]
        public async Task<IActionResult> GetAllPlayers() {
            var players = await _playerRepository.GetPlayers();
            var playerDtos = players.Select(p => new PlayerDto {
                Id = p.Id,
                Account = p.Account,
                AccountType = p.AccountType,
                DateCreate = p.DateCreate
            }).ToList();
            return Ok(playerDtos);
        }

        /// <summary>
        /// 获取所有玩家(自动映射)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllPlayersAuto")]
        public async Task<IActionResult> GetAllPlayersAuto() {
            var players = await _playerRepository.GetPlayers();
            var playerDtos = _mapper.Map<IEnumerable<PlayerDto>>(players);
            return Ok(playerDtos);
        }

        [HttpGet("GetPlayersByCondition")]
        public async Task<IActionResult> GetPlayersByCondition([FromQuery] PlayerParameter parameter) {
            var players = await _playerRepository.GetPlayersByCondition(parameter);
            var playerDtos = _mapper.Map<IEnumerable<PlayerDto>>(players);
            return Ok(playerDtos);
        }

        [HttpGet("GetPlayersByPaged")]
        public IActionResult GetPlayersByPaged([FromQuery] PlayerParameter parameter) {
            var players = _playerRepository.GetPlayersByConditionPro(parameter);
            Response.Headers.Add("X-Pagination" , JsonConvert.SerializeObject(players.pagedMetaData));
            var playerDtos = _mapper.Map<IEnumerable<PlayerDto>>(players);
            return Ok(playerDtos);
        }

        /// <summary>
        /// 根据Id获取玩家信息
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{id}" , Name = "PlayerById")]
        //[Route("GetPlayerById")]
        public async Task<IActionResult> GetPlayerById(Guid id) {
            var player = await _playerRepository.GetPlayerById(id);
            if (player == null) {
                return NotFound();
            }
            var playerDto = _mapper.Map<PlayerDto>(player);
            return Ok(playerDto);
        }

        [HttpGet("{id}/withCharacters")]
        public async Task<IActionResult> GetPlayerWithCharacters(Guid id) {
            var player = await _playerRepository.GetPlayerWithCharacters(id);
            if (player == null) {
                return NotFound();
            }
            var result = _mapper.Map<PlayerWithCharactersDto>(player);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody] PlayerForCreationDto player) {
            if (!ModelState.IsValid) {
                return BadRequest("无效请求");
            }
            // 将PlayerForCreationDto 映射为 Player对象
            var playerEntity = _mapper.Map<Player>(player);
          
            await _playerRepository.Create(playerEntity);
            await _playerRepository.SaveChangesAsync();

            var createdPlayer = _mapper.Map<PlayerDto>(playerEntity);

            return CreatedAtRoute("PlayerById", new { id = createdPlayer.Id }, createdPlayer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayers(Guid id , [FromBody] PlayerForUpdateDto player) {
            if (!ModelState.IsValid) {
                return BadRequest("无效请求对象");
            }
            var playerEntity = await _playerRepository.GetPlayerById(id);
            if (playerEntity is null) {
                return NotFound("未找到该玩家");
            }
            _mapper.Map(player , playerEntity);
            //_mapper.Map<Player>(playerEntity);
            _playerRepository.Update(playerEntity);
            await _playerRepository.SaveChangesAsync();

            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(Guid id) {
            var players = await _playerRepository.GetPlayerWithCharacters(id);
            if (players is null) {
                return NotFound("未找到该玩家");
            }

            if (players.Characters.Any()) {
                return NotFound("该玩家有关联角色 ，不能删除");
            }

            _playerRepository.Delete(players);
            await _playerRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(Guid guid, string account) {
            var request = new PlayerDto {
                Id = guid,
                Account = account
            };
            var result = _playerService.SearchPlayers(request);
            return Ok(result);
        }
    }
}
