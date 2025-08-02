using AutoMapper;
using Blf.Net8.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blf2.Net8.Web.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UtilsController : ControllerBase {
        /// <summary>
        /// 注入仓储
        /// </summary>
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public UtilsController(IPlayerRepository playerRepository, IMapper mapper) {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        [HttpPost("inserDataByNumber")]
        public async Task<IActionResult> InsertDataToDB(int number) {
            var (success , insertCount) = await  _playerRepository.InsertDataToDB(number);
            if (success) {
                return Ok($"成功插入了 {insertCount} 条数据");
            }
            else {
                return BadRequest(new { Message = $"插入失败: {insertCount}" });
            }
        }
    }
}
